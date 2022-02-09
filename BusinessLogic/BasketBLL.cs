using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Models.Requests;
using Models.Responses;
using System;
using System.Net;

namespace BusinessLogic
{
    public class BasketBLL : IBasketBLL
    {
        private readonly IBasketDAL _basketDAL;

        public BasketBLL(IBasketDAL basketDAL)
        {
            _basketDAL = basketDAL;
        }

        public ResponseModel<string> CreateCustomer(CreateCustomerRequestModel customer)
        {
            var customerId = _basketDAL.InsertCustomer(customer);
            return new ResponseModel<string> 
            {
                Status = HttpStatusCode.OK,
                Response = $"Assigned ID: {customerId}, use it for your basket!"
            };
        }

        public ResponseModel<string> AddProductToBasket(int id, AddProductRequestModel product)
        {
            if (!_basketDAL.IsCustomerIdValid(id))
            {
                return new ResponseModel<string>
                {
                    Status = HttpStatusCode.NotFound,
                    Response = $"Customer with id: {id} not found!"
                };
            }

            _basketDAL.AddProductToBasket(id, product);


            return new ResponseModel<string>();
        }
    }
}
