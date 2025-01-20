using AVIVA.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AVIVA.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepositoryAsync<Order>
    {
        Task<List<Order>> GetAllOrderAsync(int pageNumber = 1, int pageSize = 10, string orderBy = "Id");
        Task<Order> GetOrderAsync(string id);
    }
}