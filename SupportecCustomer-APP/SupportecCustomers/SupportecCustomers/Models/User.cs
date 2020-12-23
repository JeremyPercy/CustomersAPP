using System;
using System.Collections.Generic;

namespace SupportecCustomers.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public Company Company { get; set; }
    }
}
