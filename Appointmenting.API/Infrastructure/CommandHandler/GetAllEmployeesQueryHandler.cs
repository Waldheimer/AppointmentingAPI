using Appointmenting.API.Application.Queries;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Application.ServiceContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.CommandHandler
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, Result<List<Employee>>>
    {
        private readonly IEmployeeRepo repo;

        public GetAllEmployeesQueryHandler(IEmployeeRepo repo)
        {
            this.repo = repo;
        }

        public async Task<Result<List<Employee>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetAllEmployees();

        }
    }
}
