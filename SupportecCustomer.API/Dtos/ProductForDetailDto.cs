using System.Collections.Generic;
using SupportecCustomer.API.Models;

namespace SupportecCustomer.API.Dtos
{
    public class ProductForDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<ItemForProductDto> Items { get; set; }
    }
}