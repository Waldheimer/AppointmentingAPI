using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Commands
{
    public class RequestAppointmentCommand : IRequest<Result<AppointmentId>>
    {
        public Appointment Appointment { get; set; }

        public RequestAppointmentCommand(Appointment appointment)
        {
            Appointment = appointment;
        }
    }
}
