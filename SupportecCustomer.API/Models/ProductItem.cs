namespace SupportecCustomer.API.Models
{
    public class ProductItem
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}