using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Infrastructure.Models;

namespace OzonEdu.MerchApi.Infrastructure.Services.Interfaces.Implementation
{
    public class EmailMockService : IEmailService
    {
        public Task Send(EmployeeNotificationEventDTO employeeNotificationEvent, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}