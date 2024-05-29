using Appointmenting.API.Application.Queries;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Infrastructure.QueryHandler.TimeSlots
{
    public class GetTimeSlotsByDateTimeQueryHandler : IRequestHandler<GetTimeSlotsByDateTimeQuery, Result<TimeSlot?>>
    {
        private readonly ITimeslotRepo _repo;

        public GetTimeSlotsByDateTimeQueryHandler(ITimeslotRepo repo)
        {
            _repo = repo;
        }

        public async Task<Result<TimeSlot?>> Handle(GetTimeSlotsByDateTimeQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByDateAndTime(request.Date, request.Time);
        }
    }
}
