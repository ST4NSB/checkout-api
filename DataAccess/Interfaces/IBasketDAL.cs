using Models.Requests;

namespace DataAccess.Interfaces
{
    public interface IBasketDAL
    {
        int InsertCustomer(CreateCustomerRequestModel customer);
        bool IsCustomerIdValid(int id);
        void AddProductToBasket(int id, AddProductRequestModel addProductRequest);
    }
}
