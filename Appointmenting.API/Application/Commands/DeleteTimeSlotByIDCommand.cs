using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Commands
{
    public class DeleteTimeSlotByIDCommand : IRequest<Result<Guid>>
    {
        public Guid ID { get; set; }

        public DeleteTimeSlotByIDCommand(Guid iD)
        {
            ID = iD;
        }
    }
}
