using Appointmenting.API.Application.Queries;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.QueryHandler.TimeSlots
{
    public class GetAllTimeSlotsQueryHandler : IRequestHandler<GetAllTimeSlotsQuery, Result<List<TimeSlot>?>>
    {
        private readonly ITimeslotRepo _repo;

        public GetAllTimeSlotsQueryHandler(ITimeslotRepo repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<TimeSlot>?>> Handle(GetAllTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllOrderedAscending();

            return result;
        }
    }
}
