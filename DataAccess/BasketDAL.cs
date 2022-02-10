using DataAccess.Interfaces;
using Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public int InsertProduct(AddProductRequestModel productReq)
        {
            var product = new ProductEntity
            {
                Name = productReq.Item,
                Price = productReq.Price
            };

            _checkoutContext.Products.Add(product);
            _checkoutContext.SaveChanges();

            return product.ProductId;
        }

        public int? GetProductId(AddProductRequestModel productReq)
        {
            var product = _checkoutContext.Products
                .Where(p => p.Name == productReq.Item
                         && p.Price == productReq.Price)
                .FirstOrDefault();

            return product?.ProductId;
        }

        public bool IsCustomerIdValid(int id)
        {
            return _checkoutContext.Customers
                .Where(c => c.CustomerId == id)
                .FirstOrDefault() != null;
        }

        public bool IsBasketClosed(int customerId)
        {
            return _checkoutContext.Customers
                .Where(c => c.CustomerId == customerId)
                .FirstOrDefault()
                .ClosedBasket;
        }

        public int? GetBasketItemId(int customerId, int productId)
        {
            var itemId = _checkoutContext.BasketHistory
                .Where(bh => bh.CustomerId == customerId
                          && bh.ProductId == productId)
                .FirstOrDefault();

            return itemId?.BasketHistoryId;
        }

        public int InsertBasketItemToHistory(int customerId, int productId)
        {
            var basketItem = new BasketHistoryEntity
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = 1
            };

            _checkoutContext.BasketHistory.Add(basketItem);
            _checkoutContext.SaveChanges();

            return basketItem.BasketHistoryId;
        }

        public void IncreaseProductQuantityInBasket(int basketItemId)
        {
            var basketItem = _checkoutContext.BasketHistory.Find(basketItemId);
            basketItem.Quantity++;
            basketItem.UpdatedDate = DateTime.Now;
            _checkoutContext.SaveChanges();
        }

        public CustomerEntity GetCustomerDetailsById(int id)
        {
            var customerDetails = _checkoutContext.Customers
                    .Where(c => c.CustomerId == id)
                    .First();

            return customerDetails;
        }

        public IEnumerable<BasketHistoryEntity> GetBasketDetails(int customerId)
        {
            var details = _checkoutContext.BasketHistory
                .Include(x => x.Product)
                .Where(bh => bh.CustomerId == customerId);

            return details;
        }

        public void UpdateCustomerBasketPaymentDetails(int customerId, ProcessCustomerPaymentRequestModel customerPaymentReq)
        {
            var customer = _checkoutContext.Customers.Find(customerId);
            customer.ClosedBasket = customerPaymentReq.Closed;
            customer.PaidBasket = customerPaymentReq.Paid;
            _checkoutContext.SaveChanges();
        }
    }
}
