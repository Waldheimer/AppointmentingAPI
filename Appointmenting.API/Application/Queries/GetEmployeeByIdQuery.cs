using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Queries
{
    public class GetEmployeeByIdQuery : IRequest<Result<Employee?>>
    {
        public EmployeeId Id { get; set; }

        public GetEmployeeByIdQuery(EmployeeId id)
        {
            Id = id;
        }
    }
}
