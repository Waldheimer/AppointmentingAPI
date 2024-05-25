using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Queries
{
    public class GetTimeSlotsFromDateToDateQuery : IRequest<Result<List<TimeSlot>?>>
    {
        public DateOnly Start { get; set; }
        public DateOnly End { get; set; }

        public GetTimeSlotsFromDateToDateQuery(DateOnly start, DateOnly end)
        {
            Start = start;
            End = end;
        }
    }
}
