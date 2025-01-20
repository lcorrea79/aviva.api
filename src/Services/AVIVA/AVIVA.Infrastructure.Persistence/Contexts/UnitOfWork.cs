using AVIVA.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace AVIVA.Infrastructure.Persistence.Contexts
{
    public class UnitOfWork(ApplicationDbContext applicationDbContext) : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext = applicationDbContext;

        public void Dispose() => _dbContext.Dispose();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
