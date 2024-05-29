using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Application.ServiceContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.CommandHandler.Employees
{
    public class DeleteEmployeeByIdCommandHandler : IRequestHandler<DeleteEmployeeByIdCommand, Result<EmployeeId>>
    {
        private readonly IEmployeeRepo _repo;
        private readonly IUnitOfWork _unit;

        public DeleteEmployeeByIdCommandHandler(IEmployeeRepo repo, IUnitOfWork unit)
        {
            _repo = repo;
            _unit = unit;
        }

        public async Task<Result<EmployeeId>> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.DeleteEmployeeById(request.ID);
            if (result.IsSuccess)
            {
                await _unit.SaveChangesAsync(cancellationToken);
            }
            return result;
        }
    }
}
