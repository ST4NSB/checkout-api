using DataAccess.Interfaces;

namespace DataAccess
{
    public class BasketDAL : IBasketDAL
    {
        private CheckoutDbContext _checkoutContext;

        public BasketDAL(CheckoutDbContext checkoutContext)
        {
            _checkoutContext = checkoutContext;
        }

        public int CreateCustomer(CreateCustomerRequest createCustomerReq)
        {
            var customer = new CustomerEntity
            {
                Name = createCustomerReq.Name,
                PaysVat = createCustomerReq.PaysVat
            };

            _checkoutContext.Customers.Add(customer);
            _checkoutContext.SaveChanges();

            return customer.CustomerId;
        }
    }
}
