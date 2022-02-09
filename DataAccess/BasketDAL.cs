using DataAccess.Interfaces;
using Models.Requests;
using System.Linq;

namespace DataAccess
{
    public class BasketDAL : IBasketDAL
    {
        private CheckoutDbContext _checkoutContext;

        public BasketDAL(CheckoutDbContext checkoutContext)
        {
            _checkoutContext = checkoutContext;
        }

        public int InsertCustomer(CreateCustomerRequestModel customerReq)
        {
            var customer = new CustomerEntity
            {
                Name = customerReq.Name,
                PaysVat = customerReq.PaysVat
            };

            _checkoutContext.Customers.Add(customer);
            _checkoutContext.SaveChanges();

            return customer.CustomerId;
        }

        public void AddProductToBasket(int id, AddProductRequestModel addProductRequest)
        {
            var product = _checkoutContext.Products
                .Where(x => x.Name == addProductRequest.Item 
                         && x.Price == addProductRequest.Price)
                .FirstOrDefault();

            if (product != null)
            {
                var x = 1;
            }
        }

        public bool IsCustomerIdValid(int id)
        {
            return _checkoutContext.Customers
                .Where(x => x.CustomerId == id)
                .FirstOrDefault() != null;
        }
    }
}
