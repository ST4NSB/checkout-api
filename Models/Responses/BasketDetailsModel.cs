using System.Collections.Generic;

namespace Models.Responses
{
    public class BasketDetailsModel
    {
        public int Id { get; set; }
        public IEnumerable<BasketItemModel> Items { get; set; }
        public decimal TotalNet { get; set; }
        public decimal TotalGross { get; set; }
        public string Customer { get; set; }
        public bool PaysVat { get; set; }
    }
}
