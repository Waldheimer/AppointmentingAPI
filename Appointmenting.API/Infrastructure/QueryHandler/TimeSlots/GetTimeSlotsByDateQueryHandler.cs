using Appointmenting.API.Application.Queries;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.QueryHandler.TimeSlots
{
    public class GetTimeSlotsByDateQueryHandler : IRequestHandler<GetTimeSlotsByDateQuery, Result<List<TimeSlot>?>>
    {
        private readonly ITimeslotRepo _repo;

        public GetTimeSlotsByDateQueryHandler(ITimeslotRepo repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<TimeSlot>?>> Handle(GetTimeSlotsByDateQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetOrderedAscendingByDay(request.Date);

            return result;
        }
    }
}
