using Appointmenting.API.Application.Commands;
using FluentValidation;

namespace Appointmenting.API.Infrastructure.Validators.Appointments
{
    public class ConfirmAppointmentValidator : AbstractValidator<ConfirmAppointmentCommand>
    {
        public ConfirmAppointmentValidator()
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
