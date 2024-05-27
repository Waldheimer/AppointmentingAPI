using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Commands
{
    public class DeleteTimeSlotsByDateCommand : IRequest<Result<List<Guid>>>
    {
        public DateOnly Date { get; set; }

        public DeleteTimeSlotsByDateCommand(DateOnly date)
        {
            Date = date;
        }
    }
}
