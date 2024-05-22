namespace Appointmenting.API.Domain.Entities
{
    public class Appointment
    {
        public AppointmentId Id { get; set; }
        public TimeSlot? TimeSlot { get; set; }
        public Employee? Employee { get; set; }
        public User? Client { get; set; }
        public bool IsRequested { get; set; } = false;
        public bool IsConfirmed { get; set; } = false;
        public bool IsCanceled { get; set; } = false;

    }
}
