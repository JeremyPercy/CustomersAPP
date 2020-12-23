using System.ComponentModel.DataAnnotations;

namespace SupportecCustomer.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "You must specify a password between 6 and 12 characters")]
        public string Password { get; set; }
    }
}