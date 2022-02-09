using Models.Requests;
using Models.Responses;

namespace BusinessLogic.Interfaces
{
    public interface IBasketBLL
    {
        ResponseModel<string> CreateCustomer(CreateCustomerRequestModel customer);
        ResponseModel<string> AddProductToBasket(int id, AddProductRequestModel product);
    }
}
