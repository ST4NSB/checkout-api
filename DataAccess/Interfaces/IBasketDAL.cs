using Models.Requests;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IBasketDAL
    {
        int InsertCustomer(CreateCustomerRequestModel customer);
        bool IsCustomerIdValid(int id);
        int InsertProduct(AddProductRequestModel productReq);
        int? GetProductId(AddProductRequestModel productReq);
        bool IsBasketClosed(int customerId);
        int? GetBasketItemId(int customerId, int productId);
        int InsertBasketItemToHistory(int customerId, int productId);
        void IncreaseProductQuantityInBasket(int basketItemId);
        CustomerEntity GetCustomerDetailsById(int id);
        IEnumerable<BasketHistoryEntity> GetBasketDetails(int customerId);
        void UpdateCustomerBasketPaymentDetails(int customerId, ProcessCustomerPaymentRequestModel customerPaymentReq);
    }
}
