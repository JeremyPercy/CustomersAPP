namespace SupportecCustomer.API.Dtos
{
    public class AddCompanyForUserDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }  
        public string BtwCode { get; set; }
        public int UserId { get; set; }
    }
}