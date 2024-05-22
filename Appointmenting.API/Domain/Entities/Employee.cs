using Appointmenting.API.Domain.ValueObjects;

namespace Appointmenting.API.Domain.Entities
{
    public class Employee
    {
        public EmployeeId EmployeeId { get; set; }
        public FirstName? FirstName { get; set; }
        public LastName? LastName { get; set; }
    }
}
