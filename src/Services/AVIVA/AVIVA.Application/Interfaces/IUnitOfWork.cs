using System;
using System.Threading;
using System.Threading.Tasks;

namespace AVIVA.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}