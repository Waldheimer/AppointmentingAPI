using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Queries
{
    public class GetTimeSlotsByDateQuery : IRequest<Result<List<TimeSlot>?>>
    {
        public DateOnly Date { get; set; }

        public GetTimeSlotsByDateQuery(DateOnly date)
        {
            Date = date;
        }
    }
}
