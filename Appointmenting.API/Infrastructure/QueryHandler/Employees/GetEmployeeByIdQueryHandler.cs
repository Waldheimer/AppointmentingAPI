using Appointmenting.API.Application.Queries;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.QueryHandler.Employees
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Result<Employee?>>
    {
        private readonly IEmployeeRepo repo;

        public GetEmployeeByIdQueryHandler(IEmployeeRepo repo)
        {
            this.repo = repo;
        }

        public async Task<Result<Employee>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetEmployeeById(request.Id);
        }
    }
}
