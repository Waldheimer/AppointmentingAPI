using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Application.ServiceContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.CommandHandler.Employees
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Result<EmployeeId>>
    {
        private readonly IEmployeeRepo _repo;
        private readonly IUnitOfWork _unit;

        public CreateEmployeeCommandHandler(IEmployeeRepo repo, IUnitOfWork unit)
        {
            _repo = repo;
            _unit = unit;
        }

        public async Task<Result<EmployeeId>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.CreateEmployee(request.Employee);
            if (result != null)
            {
                await _unit.SaveChangesAsync(cancellationToken);
            }
            return result!;
        }
    }
}
