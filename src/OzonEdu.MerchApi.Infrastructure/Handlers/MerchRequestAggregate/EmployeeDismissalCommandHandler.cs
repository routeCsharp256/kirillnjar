using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchApi.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchApi.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchApi.Infrastructure.Commands.EmployeeDismissalCommand;

namespace OzonEdu.MerchApi.Infrastructure.Handlers.MerchRequestAggregate
{
    public class EmployeeDismissalCommandHandler : IRequestHandler<EmployeeDismissalCommand>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;

        public EmployeeDismissalCommandHandler(IMerchRequestRepository merchRequestRepository)
        {
            _merchRequestRepository = merchRequestRepository;
        }

        public async Task<Unit> Handle(EmployeeDismissalCommand command, CancellationToken cancellationToken)
        {
            var requests =  await _merchRequestRepository.Get(
                Email.Create(command.Employee.Email), 
                MerchRequestStatus.AwaitingDelivery,
                cancellationToken);
            foreach (var request in requests)
            {
                request.Employee.Dissmiss();
                request.SetAsCanceled(MerchRequestDateTime.Create(DateTime.UtcNow));
                await _merchRequestRepository.Update(request, cancellationToken);
            }
            return Unit.Value;
        }
    }
}

