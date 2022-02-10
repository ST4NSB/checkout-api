using System;

namespace DataAccess
{
    public class CustomerEntity
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public bool PaysVat { get; set; }
        public bool ClosedBasket { get; set; }
        public bool PaidBasket { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}