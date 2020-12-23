using System;
using System.Collections.Generic;

namespace SupportecCustomers.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public List<Item> Items { get; set; }
    }
}
