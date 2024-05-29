using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Infrastructure.Database;
using FluentValidation;

namespace Appointmenting.API.Infrastructure.Validators.Appointments
{
    public class RequestAppointmentValidator : AbstractValidator<RequestAppointmentCommand>
    {
        public RequestAppointmentValidator()
        {
            RuleFor(r => r.Appointment).Must(data =>
            {
                var valid = data.TimeSlot.day >= DateOnly.FromDateTime(DateTime.Today)
                    && data.TimeSlot.time >= TimeOnly.FromTimeSpan(TimeSpan.FromHours(8))
                    && data.TimeSlot.time <= TimeOnly.FromTimeSpan(TimeSpan.FromHours(18));
                return valid;
            }).WithMessage("Error in Timeslot");
        }
    }
}
