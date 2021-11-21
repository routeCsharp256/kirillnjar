using OzonEdu.MerchApi.Enums;

namespace OzonEdu.MerchApi.Services.Models
{
    public class EmployeeNotificationEventDTO
    {
        public string EmployeeEmail { get; set; }

        public string EmployeeName { get; set; }

        public EmployeeEventType EventType { get; set; }

        public Payload Payload { get; set; }
    }

    public class Payload
    {
        public int MerchType { get; set; }
    }

}