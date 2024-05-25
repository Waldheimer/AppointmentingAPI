using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Queries
{
    public class GetTimeSlotsFromTimeToTimeOnDateQuery : IRequest<Result<List<TimeSlot>?>>
    {
        public DateOnly Date { get; set; }
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }

        public GetTimeSlotsFromTimeToTimeOnDateQuery(DateOnly date, TimeOnly start, TimeOnly end)
        {
            Date = date;
            Start = start;
            End = end;
        }
    }
}
