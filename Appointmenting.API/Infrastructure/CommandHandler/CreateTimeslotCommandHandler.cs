using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.CommandHandler
{
    public class CreateTimeslotCommandHandler : IRequestHandler<CreateTimeSlotCommand, Result<Guid>>
    {
        private ITimeslotRepo _repo;

        public CreateTimeslotCommandHandler(ITimeslotRepo repo)
        {
            _repo = repo;
        }

        public async Task<Result<Guid>> Handle(CreateTimeSlotCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.Create(request.Value);
            return result;
        }
    }
}
