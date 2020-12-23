namespace SupportecCustomer.API.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }  
        public string BtwCode { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}