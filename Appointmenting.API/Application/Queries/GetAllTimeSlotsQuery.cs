using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Queries
{
    public class GetAllTimeSlotsQuery : IRequest<Result<List<TimeSlot>?>>
    {
    }
}
