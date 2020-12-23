using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SupportecCustomer.API.Models;

namespace SupportecCustomer.API.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void addItemsToProduct(int productId, IEnumerable<int> itemsId)
        {
            var productItems = new List<ProductItem>();

            foreach(int itemId in itemsId)
            {
                productItems.Add(new ProductItem()
                {
                    ProductId = productId,
                    ItemId = itemId
                });
            }

            _context.AddRange(productItems);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            var items = await _context.Items.ToListAsync();

            return items;
        }

        public async Task<IEnumerable<Item>> GetItemsFromProduct(int id)
        {
            var items = await _context.Items.Where(i => i.ProductItems
                                    .Any(p => p.ProductId == id)).ToListAsync();
            return items;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products.Include(ProductItem => ProductItem.ProductItems).ThenInclude(i => i.Item).FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts(int id)
        {
            var products = await _context.Products.Where(p => p.UserId == id).ToListAsync();

            return products;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}