namespace Appointmenting.API.Domain.Entities
{
    public readonly record struct AppointmentId(Guid Value)
    {
        public static AppointmentId Empty => new(Guid.Empty);
        public static AppointmentId CreateNew() => new(Guid.NewGuid());
    }
    
}
