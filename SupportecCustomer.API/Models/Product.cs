using System.Collections.Generic;

namespace SupportecCustomer.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<ProductItem> ProductItems { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}