using FluentAssertions;
using Models.Requests;
using Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class BasketIntegrationTests : IDisposable
    {
        private readonly HttpClient _client;
        private const string ApiEndpoint = "/api/baskets";

        public BasketIntegrationTests()
        {
            _client = new TestClientProvider().Client;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        [Fact]
        public async Task CreateCustomer()
        {
            var response = await _client.PostAsync($"{ApiEndpoint}"
                , new StringContent(
                    JsonConvert.SerializeObject(new CreateCustomerRequestModel()
                    {
                        Name = "SimpleTestName",
                        PaysVat = true
                    }),
            Encoding.UTF8,
            "application/json"));

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { false, "tomato", 20M, "juice", 10M, 1, 1, 30M, 30M },
            new object[] { true, "tomato", 20M, "juice", 10M, 1, 1, 30M, 33M },
            new object[] { false, "tomato", 20M, "tomato", 20M, 2, 2, 40M, 40M } // first == last
        };

        [Theory]
        [MemberData(nameof(Data))]
        public async Task ProcessSimpleBasket(bool paysVat, 
                                              string nameItem1, 
                                              decimal priceItem1,
                                              string nameItem2,
                                              decimal priceItem2,
                                              int quantityItem1, 
                                              int quantityItem2,
                                              decimal totalNetExpected,
                                              decimal totalGrossExpected)
        {
            var customerId = await GetCustomerId(paysVat);
            await AddProductToBasket(customerId, nameItem1, priceItem1);
            await AddProductToBasket(customerId, nameItem2, priceItem2);

            var response = await _client.GetAsync($"{ApiEndpoint}/{customerId}");
            var basketDetails = await response.Content.ReadAsStringAsync();
            var basketDetailsDeserialized = JsonConvert.DeserializeObject<BasketDetailsModel>(basketDetails);

            basketDetailsDeserialized.Items.First().Quantity.Should().Be(quantityItem1);
            basketDetailsDeserialized.Items.Last().Quantity.Should().Be(quantityItem2);
            basketDetailsDeserialized.TotalNet.Should().Be(totalNetExpected);
            basketDetailsDeserialized.TotalGross.Should().Be(totalGrossExpected);
        }

        [Fact]
        public async Task CloseBasket()
        {
            var customerId = await GetCustomerId(true);
            var response = await _client.PatchAsync($"{ApiEndpoint}/{customerId}"
                , new StringContent(
                    JsonConvert.SerializeObject(new ProcessCustomerPaymentRequestModel()
                    {
                        Closed = true,
                        Paid = true
                    }),
            Encoding.UTF8,
            "application/json"));

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private async Task<int> GetCustomerId(bool paysVat)
        {
            var response = await _client.PostAsync($"{ApiEndpoint}"
                , new StringContent(
                    JsonConvert.SerializeObject(new CreateCustomerRequestModel()
                    {
                        Name = "SimpleTestName",
                        PaysVat = paysVat
                    }),
            Encoding.UTF8,
            "application/json"));

            var customer = await response.Content.ReadAsStringAsync();
            var customerId = JsonConvert.DeserializeObject<CustomerCreatedModel>(customer).Id;
            return customerId;
        }

        private async Task AddProductToBasket(int id, string item, decimal price)
        {
            var response = await _client.PutAsync($"{ApiEndpoint}/{id}/article-line"
                , new StringContent(
                    JsonConvert.SerializeObject(new AddProductRequestModel()
                    {
                        Item = item,
                        Price = price
                    }),
            Encoding.UTF8,
            "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}
