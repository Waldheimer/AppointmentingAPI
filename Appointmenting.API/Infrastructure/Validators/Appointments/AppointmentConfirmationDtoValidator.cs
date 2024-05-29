using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.Entities;
using FluentValidation;

namespace Appointmenting.API.Infrastructure.Validators.Appointments
{
    public class AppointmentConfirmationDtoValidator : AbstractValidator<AppointmentConfirmationDTO>
    {
        public TimeSlot? TimeSlot { get; private set; }
        public Employee? Employee { get; private set; }
        public User? User { get; private set; }

        public AppointmentConfirmationDtoValidator(IUserRepo userRepo, IEmployeeRepo employeeRepo)
        {
            DateOnly date;
            TimeOnly time;

            RuleFor(a => a).Must(data =>
            {
                var dateresult = DateOnly.TryParse(data.Date, out date);
                var timeresult = TimeOnly.TryParse(data.Time, out time);
                if (dateresult && timeresult) TimeSlot = new TimeSlot(date, time);
                return dateresult && timeresult;
            }).WithMessage("Not a valid Date representation");

            RuleFor(r => r.User).Must(data =>
            {
                var result = userRepo.GetUserById(data);
                User = result == null ? null : result.Result.Value;
                return result != null;
            }).WithMessage("Error finding User");

            RuleFor(e => e.Employee).Must(data =>
            {
                var result = employeeRepo.GetEmployeeById(data);
                Employee = result == null ? null : result.Result.Value;
                return result != null;
            }).WithMessage("Error finding Employee");
        }
    }
}
