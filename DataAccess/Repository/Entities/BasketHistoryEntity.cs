using System;

namespace DataAccess
{
    public class BasketHistoryEntity
    {
        public int BasketHistoryId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public CustomerEntity Customer { get; set; }
        public ProductEntity Product { get; set; }
    }
}