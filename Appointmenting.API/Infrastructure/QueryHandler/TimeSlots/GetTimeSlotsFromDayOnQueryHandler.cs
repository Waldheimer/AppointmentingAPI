using Appointmenting.API.Application.Queries;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.QueryHandler.TimeSlots
{
    public class GetTimeSlotsFromDayOnQueryHandler : IRequestHandler<GetTimeSlotsFromDateOnQuery, Result<List<TimeSlot>?>>
    {
        private readonly ITimeslotRepo _repo;

        public GetTimeSlotsFromDayOnQueryHandler(ITimeslotRepo repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<TimeSlot>?>> Handle(GetTimeSlotsFromDateOnQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetOrderedAscendingFromDateOn(request.Date);
        }
    }
}
