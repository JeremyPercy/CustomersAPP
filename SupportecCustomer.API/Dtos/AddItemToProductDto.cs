using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SupportecCustomer.API.Dtos
{
    public class AddItemToProductDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public IEnumerable<int> ItemId { get; set; }
    }
}