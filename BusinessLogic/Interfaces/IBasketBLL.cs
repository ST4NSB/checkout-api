using Models.Requests;
using Models.Responses;

namespace BusinessLogic.Interfaces
{
    public interface IBasketBLL
    {
        ResponseModel<string> CreateCustomer(CreateCustomerRequestModel customerReq);
        ResponseModel<string> AddProductToBasket(int id, AddProductRequestModel productReq);
        ResponseModel<BasketDetails> GetBasketDetails(int id);
        ResponseModel<string> ProcessCustomerPayment(int id, ProcessCustomerPaymentRequestModel basketProcessReq);
    }
}
