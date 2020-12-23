using System.ComponentModel.DataAnnotations;

namespace SupportecCustomer.API.Dtos
{
    public class AddProductForUserDto
    {
        [Required]
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        
        [Required]
        public int UserId { get; set; }
    }
}