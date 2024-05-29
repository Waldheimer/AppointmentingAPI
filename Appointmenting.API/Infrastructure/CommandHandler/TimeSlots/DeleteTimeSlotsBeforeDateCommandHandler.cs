using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Application.ServiceContracts;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.CommandHandler.TimeSlots
{
    public class DeleteTimeSlotsBeforeDateCommandHandler : IRequestHandler<DeleteTimeSlotsBeforeDateCommand, Result<List<Guid>>>
    {
        private readonly ITimeslotRepo _repo;
        private readonly IUnitOfWork _unit;

        public DeleteTimeSlotsBeforeDateCommandHandler(ITimeslotRepo repo, IUnitOfWork unit)
        {
            _repo = repo;
            _unit = unit;
        }

        public async Task<Result<List<Guid>>> Handle(DeleteTimeSlotsBeforeDateCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.DeleteBeforeDate(request.Date);
            if (result.IsSuccess)
            {
                await _unit.SaveChangesAsync(cancellationToken);
            }
            return result;
        }
    }
}
