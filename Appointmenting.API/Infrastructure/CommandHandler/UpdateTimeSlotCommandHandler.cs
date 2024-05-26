using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.CommandHandler
{
    public class UpdateTimeSlotCommandHandler : IRequestHandler<UpdateTimeSlotCommand, Result<Guid>>
    {
        private readonly ITimeslotRepo _repo;

        public UpdateTimeSlotCommandHandler(ITimeslotRepo repo)
        {
            _repo = repo;
        }

        public async Task<Result<Guid>> Handle(UpdateTimeSlotCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Update(request.TimeSlot);
        }
    }
}
