using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Commands
{
    public class UpdateTimeSlotCommand : IRequest<Result<Guid>>
    {
        public UpdateTimeslotDTO TimeSlot { get; set; }

        public UpdateTimeSlotCommand(UpdateTimeslotDTO timeslot)
        {
            TimeSlot = timeslot;
        }
    }
}
