using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Application.ServiceContracts;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.CommandHandler.TimeSlots
{
    public class CreateTimeslotCommandHandler : IRequestHandler<CreateTimeSlotCommand, Result<Guid>>
    {
        private ITimeslotRepo _repo;
        private readonly IUnitOfWork _unit;

        public CreateTimeslotCommandHandler(ITimeslotRepo repo, IUnitOfWork unit)
        {
            _repo = repo;
            _unit = unit;
        }

        public async Task<Result<Guid>> Handle(CreateTimeSlotCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.Create(request.Value);
            if (result.IsSuccess)
            {
                await _unit.SaveChangesAsync(cancellationToken);
            }
            return result;
        }
    }
}
