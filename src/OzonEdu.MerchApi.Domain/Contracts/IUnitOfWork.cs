using System;
using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchApi.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ValueTask StartTransaction(CancellationToken cancellationToken);
        
        Task SaveChanges(CancellationToken cancellationToken);
        
        Task Rollback(CancellationToken cancellationToken);
    }
}