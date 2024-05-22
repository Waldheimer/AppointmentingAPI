namespace Appointmenting.API.Domain.Entities
{
    public readonly record struct EmployeeId(Guid value)
    {
        public static EmployeeId Empty => new(Guid.Empty);
        public static EmployeeId CreateNew() => new(Guid.NewGuid());
    }
}
