using BusinessLogic;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Models.Requests;
using Models.Responses;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;

namespace NUnitTests
{
    [TestFixture]
    public class BasketUnitTests
    {
        private Mock<IBasketDAL> _basketDAL;
        private Mock<ILogger<IBasketBLL>> _logger;
        private BasketBLL _basketBLL;

        [SetUp]
        public void Setup()
        {
            _basketDAL = new Mock<IBasketDAL>();
            _logger = new Mock<ILogger<IBasketBLL>>();
            _basketBLL = new BasketBLL(_logger.Object, _basketDAL.Object);

            // [assembly: InternalsVisibleTo("NUnitTests")] -> in BusinessLogic.AssemblyInfo.cs
        }

        [TestCase(0, 0)]
        [TestCase(1, 1.1)]
        [TestCase(30, 33)]
        [TestCase(87, 95.7)]
        public void CalculateTotalGrossAmountTests(decimal netAmount, decimal expected)
        {
            var actual = HelperFunctions.CalculateTotalGrossAmount(netAmount);
            actual.Should().Be(expected);
        }

        [TestCase("water", 10, 1, 2, 1)]
        [TestCase("razer keyboard", 200, null, 1, 1)]
        public void MergeRequestedProductInDbTests(string item, decimal price, int? getReturn, int insertReturn, int expected)
        {
            var reqModel = new AddProductRequestModel
            {
                Item = item,
                Price = price
            };

            _basketDAL.Setup(x => x.GetProductId(reqModel)).Returns(getReturn);
            _basketDAL.Setup(x => x.InsertProduct(reqModel)).Returns(insertReturn);

            var actual = _basketBLL.MergeRequestedProductInDb(reqModel);
            actual.Should().Be(expected);
        }

        [TestCase(1, 1, false, false, HttpStatusCode.NotFound, "Customer with id: 1 not found!", null)]
        [TestCase(1, -1, true, false, HttpStatusCode.Forbidden, "You can't add a product with negative price! (price: -1)", null)]
        [TestCase(1, 1, true, true, HttpStatusCode.Forbidden, "The basket for this customer (ID: 1) is closed!", null)]
        [TestCase(1, 1, true, false, HttpStatusCode.OK, null, "Added product to your basket!")]
        public void AddProductToBasketTests(int id,
                                            decimal price,
                                            bool customerValidity,
                                            bool isClosed,
                                            HttpStatusCode status,
                                            string errorMessage,
                                            string result)
        {
            var reqModel = new AddProductRequestModel
            {
                Item = "TestItem",
                Price = price
            };

            var expected = new ResponseModel<string>
            {
                Status = status,
                ErrorMessage = errorMessage,
                Result = result
            };

            _basketDAL.Setup(x => x.IsCustomerIdValid(id)).Returns(customerValidity);
            _basketDAL.Setup(x => x.GetProductId(reqModel)).Returns(1);
            _basketDAL.Setup(x => x.IsBasketClosed(id)).Returns(isClosed);
            _basketDAL.Setup(x => x.GetBasketItemId(1, 1)).Returns(1);

            var actual = _basketBLL.AddProductToBasket(id, reqModel);
            actual.Should().BeEquivalentTo(expected);
        }

        // couldn't find a way to pass decimal array in Testcase
        [TestCase(1, false, new int[] { 1, 1 }, 1.0, 1.0, 2.0 )] 
        [TestCase(1, false, new int[] { 1, 1 }, 0.0, 1.5, 1.5 )] 
        [TestCase(1, false, new int[] { 2, 1 }, 10.0, 0.25, 20.25 )] 
        [TestCase(1, true, new int[] { 10, 2 }, 1.0, 10.0, 30.0 )] 
        [TestCase(1, true, new int[] { 10, 10 }, 50.0, 50.0, 1000.0 )] 
        public void GetBasketDetailsTests(int id, bool paysVat, int[] quantities, decimal priceFirstItem, decimal priceSecondItem, decimal totalNet)
        {
            var customerDetails = new CustomerEntity
            {
                Name = "TestCustomer",
                PaysVat = paysVat
            };

            var basketDetails = new List<BasketHistoryEntity>
            {
                new BasketHistoryEntity
                {
                    Quantity = quantities[0],
                    Product = new ProductEntity
                    {
                        Name = "ItemTest1",
                        Price = priceFirstItem
                    }
                },
                new BasketHistoryEntity
                {
                    Quantity = quantities[1],
                    Product = new ProductEntity
                    {
                        Name = "ItemTest2",
                        Price = priceSecondItem
                    }
                },
            };

            var expected = new ResponseModel<BasketDetailsModel>
            {
                Status = HttpStatusCode.OK,
                Result = new BasketDetailsModel
                {
                    Id = id,
                    Customer = "TestCustomer",
                    Items = new List<BasketItemModel>
                    {
                        new BasketItemModel
                        {
                            Quantity = quantities[0],
                            Item = "ItemTest1",
                            Price = priceFirstItem
                        },
                        new BasketItemModel
                        {
                            Quantity = quantities[1],
                            Item = "ItemTest2",
                            Price = priceSecondItem
                        }
                    },
                    TotalNet = totalNet,
                    TotalGross = paysVat ? HelperFunctions.CalculateTotalGrossAmount(totalNet) : totalNet,
                    PaysVat = paysVat
                }
            };

            _basketDAL.Setup(x => x.IsCustomerIdValid(id)).Returns(true);
            _basketDAL.Setup(x => x.GetCustomerDetailsById(id)).Returns(customerDetails);
            _basketDAL.Setup(x => x.GetBasketDetailsById(id)).Returns(basketDetails);

            var actual = _basketBLL.GetBasketDetails(id);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}