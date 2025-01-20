using AVIVA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AVIVA.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepositoryAsync<Product>
    {
        Task<List<Product>> GetAllProductAsync();

        Task<List<Product>> GetAllProductByNameAsync(string queryName);
    }
}