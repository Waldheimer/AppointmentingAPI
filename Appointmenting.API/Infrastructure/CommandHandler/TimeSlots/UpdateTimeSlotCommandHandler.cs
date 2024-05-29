using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Application.ServiceContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.CommandHandler.TimeSlots
{
    public class UpdateTimeSlotCommandHandler : IRequestHandler<UpdateTimeSlotCommand, Result<Guid>>
    {
        private readonly ITimeslotRepo _repo;
        private readonly IUnitOfWork _unit;

        public UpdateTimeSlotCommandHandler(ITimeslotRepo repo, IUnitOfWork unit)
        {
            _repo = repo;
            _unit = unit;
        }

        public async Task<Result<Guid>> Handle(UpdateTimeSlotCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.Update(request.TimeSlot);
            if (result.IsSuccess)
            {
                await _unit.SaveChangesAsync(cancellationToken);
            }
            return result;
        }
    }
}
