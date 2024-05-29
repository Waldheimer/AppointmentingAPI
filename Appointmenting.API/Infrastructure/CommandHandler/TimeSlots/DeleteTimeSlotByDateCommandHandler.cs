using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Application.ServiceContracts;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.CommandHandler.TimeSlots
{
    public class DeleteTimeSlotByDateCommandHandler : IRequestHandler<DeleteTimeSlotsByDateCommand, Result<List<Guid>>>
    {
        private readonly ITimeslotRepo _repo;
        private readonly IUnitOfWork _unit;

        public DeleteTimeSlotByDateCommandHandler(ITimeslotRepo repo, IUnitOfWork unit)
        {
            _repo = repo;
            _unit = unit;
        }

        public async Task<Result<List<Guid>>> Handle(DeleteTimeSlotsByDateCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.DeleteByDate(request.Date);
            if (result.IsSuccess && result.Value.Count() > 0)
            {
                await _unit.SaveChangesAsync(cancellationToken);
            }
            return result;
        }
    }
}
