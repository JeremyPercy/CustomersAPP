using System;
using SupportecCustomer.API.Models;

namespace SupportecCustomer.API.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public String Company { get; set; }
    }
}