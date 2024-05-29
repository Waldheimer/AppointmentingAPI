using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Commands
{
    public class DeleteEmployeeByIdCommand : IRequest<Result<EmployeeId>>
    {
        public EmployeeId ID { get; set; }

        public DeleteEmployeeByIdCommand(EmployeeId iD)
        {
            ID = iD;
        }
    }
}
