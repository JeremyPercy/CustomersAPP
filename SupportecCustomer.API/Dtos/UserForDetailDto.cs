using System;
using System.Collections.Generic;
using SupportecCustomer.API.Models;

namespace SupportecCustomer.API.Dtos
{
    public class UserForDetailDto
    {
        public int id { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public CompanyForDetailDto Company { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotosForDetailDto> Photos { get; set; }
        public ICollection<ProductForDetailDto> Products { get; set; }
    }
}