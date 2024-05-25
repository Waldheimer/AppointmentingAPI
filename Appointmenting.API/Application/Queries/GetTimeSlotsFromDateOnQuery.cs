using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Queries
{
    public class GetTimeSlotsFromDateOnQuery : IRequest<Result<List<TimeSlot>?>>
    {
        public DateOnly Date { get; set; }

        public GetTimeSlotsFromDateOnQuery(DateOnly date)
        {
            Date = date;
        }
    }
}
