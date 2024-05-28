using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.ValueObjects;

namespace Appointmenting.API.Domain.Entities
{
    public class Employee
    {
        public EmployeeId EmployeeId { get; set; }
        public FirstName? FirstName { get; set; }
        public LastName? LastName { get; set; }

        public static Employee Default => new Employee
        {
            EmployeeId = new EmployeeId(Guid.Empty),
            FirstName = FirstName.Default,
            LastName = LastName.Default
        };
        public static Employee FromId(EmployeeId id) => new Employee
        {
            EmployeeId = id,
            FirstName = FirstName.Default,
            LastName = LastName.Default
        };
        public static Employee FromDTO(EmployeeDTO dto) => new Employee
        {
            EmployeeId = EmployeeId.CreateNew(),
            FirstName = new FirstName(dto.FirstName),
            LastName = new LastName(dto.LastName)
        };
    }
}
