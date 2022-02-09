using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Interfaces;
using System;

namespace BusinessLogic
{
    public class BasketBLL : IBasketBLL
    {
        private IBasketDAL _basketDAL;

        public BasketBLL(IBasketDAL basketDAL)
        {
            _basketDAL = basketDAL;
        }

        public int CreateCustomer(CreateCustomerRequest createCustomerRequest)
        {
            return _basketDAL.CreateCustomer(createCustomerRequest);
        }

    }
}
