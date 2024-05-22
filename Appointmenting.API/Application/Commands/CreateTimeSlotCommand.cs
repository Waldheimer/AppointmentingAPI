using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Commands
{
    public class CreateTimeSlotCommand : IRequest<Result<Guid>>
    {
        public TimeslotDTO Value { get; set; }

        public CreateTimeSlotCommand(TimeslotDTO value)
        {
            Value = value;
        }
    }
}
