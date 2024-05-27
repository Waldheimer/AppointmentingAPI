using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Commands
{
    public class DeleteTimeSlotsBeforeDateCommand : IRequest<Result<List<Guid>>>
    {
        public DateOnly Date { get; set; }

        public DeleteTimeSlotsBeforeDateCommand(DateOnly date)
        {
            Date = date;
        }
    }
}
