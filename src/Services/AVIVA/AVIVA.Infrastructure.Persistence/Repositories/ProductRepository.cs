using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Domain.Entities;
using AVIVA.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVIVA.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : GenericRepositoryAsync<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            var productList = await _dbContext.Products
                                              .ToListAsync();


            return productList;
        }

        public Task<List<Product>> GetAllProductByNameAsync(string queryName)
        {
            var productList = _dbContext.Products
                .Where(p => EF.Functions.Like(p.Name, $"%{queryName}%") ||
                            EF.Functions.Like(p.Details, $"%{queryName}%"))
                .ToListAsync();



            return productList;
        }


    }
}