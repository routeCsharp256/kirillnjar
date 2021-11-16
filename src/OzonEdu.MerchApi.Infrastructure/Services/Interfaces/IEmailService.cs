using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchApi.Infrastructure.Models;

namespace OzonEdu.MerchApi.Infrastructure.Services.Interfaces
{
    public interface IEmailService
    {
        public Task Send(EmployeeNotificationEventDTO employeeNotificationEvent, CancellationToken cancellationToken);
    }
}