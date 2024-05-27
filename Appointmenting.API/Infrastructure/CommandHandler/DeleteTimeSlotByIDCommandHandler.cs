using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Application.ServiceContracts;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.CommandHandler
{
    public class DeleteTimeSlotByIDCommandHandler : IRequestHandler<DeleteTimeSlotByIDCommand, Result<Guid>>
    {
        private readonly ITimeslotRepo _repo;
        private readonly IUnitOfWork _unit;

        public DeleteTimeSlotByIDCommandHandler(ITimeslotRepo repo, IUnitOfWork unit)
        {
            _repo = repo;
            _unit = unit;
        }

        public async Task<Result<Guid>> Handle(DeleteTimeSlotByIDCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.DeleteById(request.ID);
            if(result.IsSuccess)
            {
                await _unit.SaveChangesAsync(cancellationToken);
            }
            return result;
        }
    }
}
