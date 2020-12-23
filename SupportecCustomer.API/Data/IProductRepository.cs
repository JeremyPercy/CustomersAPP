using System.Collections.Generic;
using System.Threading.Tasks;
using SupportecCustomer.API.Models;

namespace SupportecCustomer.API.Data
{
    public interface IProductRepository : IGenericRepository
    {
        Task<IEnumerable<Product>> GetProducts(int id);
        Task<Product> GetProduct(int id);
        Task<IEnumerable<Item>> GetItems();

        void addItemsToProduct(int productId, IEnumerable<int> itemsId);

        Task<IEnumerable<Item>> GetItemsFromProduct(int id);
    }
}