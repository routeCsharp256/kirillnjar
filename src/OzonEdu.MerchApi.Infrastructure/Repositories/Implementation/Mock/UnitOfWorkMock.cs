using System;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Infrastructure.Repositories.Implementation.Mock
{
    public class UnitOfWorkMock : IUnitOfWork
    {
        public ValueTask StartTransaction(CancellationToken token)
        {
            return ValueTask.CompletedTask;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        void IDisposable.Dispose()
        {
        }
    }
}