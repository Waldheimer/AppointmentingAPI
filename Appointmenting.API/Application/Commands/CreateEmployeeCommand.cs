using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Commands
{
    public class CreateEmployeeCommand : IRequest<Result<EmployeeId>>
    {
        public Employee Employee { get; set; }

        public CreateEmployeeCommand(Employee employee)
        {
            Employee = employee;
        }
    }
}
