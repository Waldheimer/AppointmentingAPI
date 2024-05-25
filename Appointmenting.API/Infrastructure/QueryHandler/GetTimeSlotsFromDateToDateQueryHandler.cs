using Appointmenting.API.Application.Queries;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.QueryHandler
{
    public class GetTimeSlotsFromDateToDateQueryHandler : IRequestHandler<GetTimeSlotsFromDateToDateQuery, Result<List<TimeSlot>?>>
    {
        private readonly ITimeslotRepo _repo;

        public GetTimeSlotsFromDateToDateQueryHandler(ITimeslotRepo repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<TimeSlot>?>> Handle(GetTimeSlotsFromDateToDateQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetOrderedAscendingFromDateToDate(request.Start, request.End);
        }
    }
}
