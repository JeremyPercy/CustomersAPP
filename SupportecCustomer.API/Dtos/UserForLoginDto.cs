using System.ComponentModel.DataAnnotations;

namespace SupportecCustomer.API.Dtos
{
    public class UserForLoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}