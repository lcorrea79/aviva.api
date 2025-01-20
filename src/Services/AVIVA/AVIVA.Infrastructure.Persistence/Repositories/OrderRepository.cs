using AVIVA.Application.Interfaces.Repositories;
using AVIVA.Domain.Entities;
using AVIVA.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AVIVA.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : GenericRepositoryAsync<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<Order> GetOrderAsync(string id)
        {
            var orderList = await _dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);

            return orderList;
        }

        public async Task<List<Order>> GetAllOrderAsync(int pageNumber = 1, int pageSize = 10, string orderBy = "Id")
        {
            var orderList = await _dbContext.Orders
                                              .ToListAsync();



            return orderList;
        }

    }
}