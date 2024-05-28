using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Commands
{
    public class ConfirmAppointmentCommand : IRequest<Result<Appointment>>
    {
        public Appointment Appointment { get; set; }

        public ConfirmAppointmentCommand(Appointment appointment)
        {
            Appointment = appointment;
        }
    }
}
