using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Queries
{
    public class GetTimeSlotsByDateTimeQuery : IRequest<Result<TimeSlot?>>
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public GetTimeSlotsByDateTimeQuery(DateOnly date, TimeOnly time)
        {
            Date = date;
            Time = time;
        }
    }
}
