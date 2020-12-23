using System;
using System.Collections.Generic;

namespace SupportecCustomer.API.Models
{
    public class User
    {
        public int id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public Company Company { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}