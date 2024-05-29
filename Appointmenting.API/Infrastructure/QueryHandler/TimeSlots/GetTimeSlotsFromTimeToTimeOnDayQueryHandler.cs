using Appointmenting.API.Application.Queries;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.QueryHandler.TimeSlots
{
    public class GetTimeSlotsFromTimeToTimeOnDayQueryHandler :
        IRequestHandler<GetTimeSlotsFromTimeToTimeOnDateQuery, Result<List<TimeSlot>?>>
    {
        private readonly ITimeslotRepo _repo;

        public GetTimeSlotsFromTimeToTimeOnDayQueryHandler(ITimeslotRepo repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<TimeSlot>?>> Handle(GetTimeSlotsFromTimeToTimeOnDateQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetOrderedAscendingFromTimeToTimeOnDate(request.Date, request.Start, request.End);
        }
    }
}
