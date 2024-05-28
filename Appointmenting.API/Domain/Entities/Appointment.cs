using Appointmenting.API.Domain.DTOs;

namespace Appointmenting.API.Domain.Entities
{
    public class Appointment
    {
        public AppointmentId Id { get; set; }
        public TimeSlot TimeSlot { get; set; } = TimeSlot.Default;
        public Employee? Employee { get; set; }
        public User? Client { get; set; }
        public bool IsRequested { get; set; } = false;
        public bool IsConfirmed { get; set; } = false;
        public bool IsCanceled { get; set; } = false;

        public static Appointment FromDTO(RequestAppointmentDTO dto)
        {
            return new Appointment
            {
                Id = new AppointmentId(Guid.NewGuid()),
                TimeSlot = new TimeSlot(DateOnly.Parse(dto.Date), TimeOnly.Parse(dto.Time)),
                Employee = Employee.Default,
                Client = null,
                IsRequested = true,
                IsConfirmed = false,
                IsCanceled = false
            };      
        }
        public static Appointment FromDTO(AppointmentConfirmationDTO dto)
        {
            return new Appointment
            {
                Id = dto.ID,
                TimeSlot = new TimeSlot(DateOnly.Parse(dto.Date), TimeOnly.Parse(dto.Time)),
                Employee = Employee.FromId(dto.Employee),
                Client = null,
                IsRequested = true,
                IsConfirmed = true,
                IsCanceled = false
            };
        }

    }

}
