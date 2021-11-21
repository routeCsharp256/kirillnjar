using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.Contracts;

namespace OzonEdu.MerchApi.Services.PipelineBehaviors.UnitOfWorkBehavior
{
    public sealed class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkBehavior(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            await _unitOfWork.StartTransaction(cancellationToken);

            try
            {
                var result = await next().ConfigureAwait(false);
                await _unitOfWork.SaveChanges(cancellationToken);
                return result;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback(cancellationToken);
                throw;
            }
        }
    }
}