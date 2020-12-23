using System.Collections.Generic;
using System.Threading.Tasks;
using SupportecCustomer.API.Models;

namespace SupportecCustomer.API.Data
{
    public interface ICustomerRepository : IGenericRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<Company> GetCompanyFromUser(int id);
    }
}