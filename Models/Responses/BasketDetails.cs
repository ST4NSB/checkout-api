using System.Collections.Generic;

namespace Models.Responses
{
    public class BasketDetails
    {
        public int Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
        public decimal TotalNet { get; set; }
        public decimal TotalGross { get; set; }
        public string Customer { get; set; }
        public bool PaysVat { get; set; }
    }
}
