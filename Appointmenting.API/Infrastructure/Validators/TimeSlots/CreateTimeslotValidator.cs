using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using FluentValidation;

namespace Appointmenting.API.Infrastructure.Validators.TimeSlots
{
    public class CreateTimeSlotValidator : AbstractValidator<CreateTimeSlotCommand>
    {

        //----------------------------------------------------------------
        //  Check if given TimeslotDTO has valid entries for Date and Time
        //  if the combined DateTime has not already passed and 
        //  that the given DateTime combination does not already exist in the DB
        //----------------------------------------------------------------
        public CreateTimeSlotValidator(ITimeslotRepo repo)
        {
            //  Check is Date is valid and not already passed
            DateOnly resultDate;
            RuleFor(c => c.Value.Date).Must(data =>
            {
                return DateOnly.TryParse(data, out resultDate) && resultDate >= DateOnly.FromDateTime(DateTime.Today);
            }).WithMessage("Value is not a valid Date representation or date already passed!");
            //  Check if Time is valid
            TimeOnly resultTime;
            RuleFor(c => c.Value.Time).Must(data =>
            {
                return TimeOnly.TryParse(data, out resultTime);
            }).WithMessage("Value is not a valid Time representation!");
            //  Check of Date/Time combination is valid and not already passed
            DateTime resultDateTime;
            RuleFor(c => c.Value).Must(data =>
            {
                return DateTime.TryParse($"{data.Date} {data.Time}", out resultDateTime) && resultDateTime >= DateTime.Now;
            }).WithMessage("Value is not a valid DateTime representation or DateTime already passed!");
            //  Check if the given DateTime already exists in the DB
            RuleFor(c => c.Value).Must(data =>
            {
                var result = repo.GetByDateAndTime(DateOnly.Parse(data.Date), TimeOnly.Parse(data.Time));
                return result != null;
            }).WithMessage("Timeslot already created!");
        }
    }
}
