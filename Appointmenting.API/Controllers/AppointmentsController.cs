using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.ServiceContracts;
using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using Appointmenting.API.Infrastructure.Validators.Appointments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointmenting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AppointmentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        //  *******************************
        //  ***** C R E A T E   ***********
        //  *******************************

        //  ----- URL       : api/appointments                  -------------------------
        //  ----- POST      : given Appointment as AppointmentDTO   ---------------------
        //  ----- Validates : the dto's data against DateOnly/TimeOnly, -----------------
        //  
        [HttpPost]
        public async Task<Result<AppointmentId>> RequestAppointment(
            [FromBody] RequestAppointmentDTO dto, 
            AppointmentDtoValidator dtoValidator,
            RequestAppointmentValidator validator)
        {
            Appointment appointment = Appointment.FromDTO(dto);
            var dtoValidationResult = dtoValidator.Validate(dto);
            if (dtoValidationResult.IsValid)
            {
                appointment.TimeSlot = dtoValidator.TimeSlot!;
                appointment.Client = dtoValidator.User;
                appointment.Employee = dtoValidator.Employee;
            }
            var command = new RequestAppointmentCommand(appointment);
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return new Result<AppointmentId>(AppointmentId.Empty, false,
                    new Error("AppointmentError.Requesting", validationResult.Errors[0].ToString()));
            }
            return await mediator.Send(command);
        }
        [HttpPost("confirmed")]
        public async Task<Result<Appointment>> ConfirmAppointment(
            [FromBody] AppointmentConfirmationDTO dto,
            AppointmentConfirmationDtoValidator dtoValidator,
            ConfirmAppointmentValidator validator)
        {
            Appointment appointment = Appointment.FromDTO(dto);
            var dtoValidationResult = dtoValidator.Validate(dto);
            if(dtoValidationResult.IsValid)
            {
                appointment.TimeSlot = dtoValidator.TimeSlot!;
                appointment.Client = dtoValidator.User;
                appointment.Employee = dtoValidator.Employee;
            }
            else
            {
                return new(null, false, new Error("DataError.Validation", dtoValidationResult.Errors[0].ToString()));
            }
            var command = new ConfirmAppointmentCommand(appointment);
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return new(appointment, false, new Error("DateError.InvalidDateTime", validationResult.Errors[0].ToString()));
            }
            return await mediator.Send(command);
        }
    }
}
