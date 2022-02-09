using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IBasketBLL
    {
        int CreateCustomer(CreateCustomerRequest createCustomerRequest);
    }
}
