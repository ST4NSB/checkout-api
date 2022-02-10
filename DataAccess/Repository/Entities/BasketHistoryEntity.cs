using System;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("ProductId")]
        public ProductEntity Product { get; set; }
    }
}