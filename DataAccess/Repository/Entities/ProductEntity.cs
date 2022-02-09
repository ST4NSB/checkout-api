using System;

namespace DataAccess
{
    public class ProductEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public BasketHistoryEntity BasketHistory { get; set; }
    }
}