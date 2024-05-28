using Appointmenting.API.Domain.Entities;

namespace Appointmenting.API.Domain.DTOs
{
    public interface IAppointmentDTO { }
    public class RequestAppointmentDTO : IAppointmentDTO
    {
        public string Date { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;
    }
    public class AppointmentConfirmationDTO : RequestAppointmentDTO
    {
        public AppointmentId ID { get; set; }
        public EmployeeId Employee { get; set; }
    }
}
