using System.Collections.Generic;

namespace SupportecCustomer.API.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductItem> ProductItems { get; set; }
    }
}