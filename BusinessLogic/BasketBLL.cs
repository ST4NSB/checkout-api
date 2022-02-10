using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using Models.Requests;
using Models.Responses;
using System.Linq;
using System.Net;

namespace BusinessLogic
{
    public class BasketBLL : IBasketBLL
    {
        private readonly ILogger<IBasketBLL> _logger;
        private readonly IBasketDAL _basketDAL;

        public BasketBLL(ILogger<IBasketBLL> logger, IBasketDAL basketDAL)
        {
            _logger = logger;
            _basketDAL = basketDAL;
        }

        public ResponseModel<string> CreateCustomer(CreateCustomerRequestModel customerReq)
        {
            var customerId = _basketDAL.InsertCustomer(customerReq);
            _logger.LogInformation($"Created customer Id: {customerId}");
            return new ResponseModel<string> 
            {
                Status = HttpStatusCode.OK,
                Result = $"Assigned ID: {customerId}, use it for your basket!"
            };
        }

        public ResponseModel<string> AddProductToBasket(int id, AddProductRequestModel productReq)
        {
            if (!_basketDAL.IsCustomerIdValid(id))
            {
                return new ResponseModel<string>
                {
                    Status = HttpStatusCode.NotFound,
                    ErrorMessage = $"Customer with id: {id} not found!"
                };
            }

            var prodId = _basketDAL.GetProductId(productReq);
            if (prodId == null)
            {
                prodId = _basketDAL.InsertProduct(productReq);
            }
            _logger.LogInformation($"Created product id: {prodId}");

            if (_basketDAL.IsBasketClosed(id))
            {
                return new ResponseModel<string>
                {
                    Status = HttpStatusCode.Forbidden,
                    ErrorMessage = $"The basket for this customer (ID: {id}) is closed!"
                };
            }
                
            var basketItemId = _basketDAL.GetBasketItemId(customerId: id, productId: (int)prodId);
            if (basketItemId == null)
            {
                var createdId = _basketDAL.InsertBasketItemToHistory(customerId: id, productId: (int)prodId);
                _logger.LogInformation($"Created basket history item id: {createdId}");
            }
            else
            {
                _basketDAL.IncreaseProductQuantityInBasket((int)basketItemId);
            }

            return new ResponseModel<string>
            {
                Status = HttpStatusCode.OK,
                Result = $"Added product to your basket!"
            };
        }

        public ResponseModel<BasketDetails> GetBasketDetails(int id)
        {
            if (!_basketDAL.IsCustomerIdValid(id))
            {
                return new ResponseModel<BasketDetails>
                {
                    Status = HttpStatusCode.NotFound,
                    ErrorMessage = $"Customer with id: {id} not found!"
                };
            }

            var customerDetails = _basketDAL.GetCustomerDetailsById(id);
            var basketDetails = _basketDAL.GetBasketDetails(id);

            var totalNet = basketDetails?.Aggregate(0m, (total, item) => total + (item.Product.Price * item.Quantity)) ?? 0m;
            var totalGross = customerDetails.PaysVat ? HelperFunctions.CalculateTotalGrossAmount(totalNet) : totalNet;
            _logger.LogInformation($"For customer (ID: {id}) --> Total Net: {totalNet}$, Total Gross: {totalGross}$");

            var res = new BasketDetails
            {
                Id = id,
                Items = basketDetails?.Select(item => new BasketItems
                {
                    Item = item.Product.Name,
                    Price = item.Product.Price,
                    Quantity = item.Quantity
                }),
                TotalNet = totalNet,
                TotalGross = totalGross,
                Customer = customerDetails.Name,
                PaysVat = customerDetails.PaysVat
            };

            return new ResponseModel<BasketDetails>
            {
                Status = HttpStatusCode.OK,
                Result = res
            };
        }

        public ResponseModel<string> ProcessCustomerPayment(int id, ProcessCustomerPaymentRequestModel basketProcessReq)
        {
            if (!_basketDAL.IsCustomerIdValid(id))
            {
                return new ResponseModel<string>
                {
                    Status = HttpStatusCode.NotFound,
                    ErrorMessage = $"Customer with id: {id} not found!"
                };
            }

            _basketDAL.UpdateCustomerBasketPaymentDetails(id, basketProcessReq);

            return new ResponseModel<string>
            {
                Status = HttpStatusCode.OK,
                Result = $"For customer (ID: {id}), " +
                         $"the basket is now {(basketProcessReq.Closed ? "closed" : "open")} and " +
                         $"the basket fee has {(basketProcessReq.Paid ? "been" : "not been")} paid!"
            };
        }
    }
}
