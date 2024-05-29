using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using FluentValidation;

namespace Appointmenting.API.Infrastructure.Validators.TimeSlots
{
    public class UpdateTimeslotValidator : AbstractValidator<UpdateTimeSlotCommand>
    {
        //----------------------------------------------------------------
        //  Check that the given ID already exist in the DB
        //  if given UpdateTimeslotDTO has valid entries for Date and Time
        //  if the combined DateTime has not already passed and 
        //----------------------------------------------------------------
        public UpdateTimeslotValidator(ITimeslotRepo repo)
        {
            //  Check if ID exists in Database
            RuleFor(t => t.TimeSlot.ID).Must((id) =>
            {
                var result = repo.GetById(id);
                Console.WriteLine(result.Result.Value.Id);
                return result.Result.Value != null;
            }).WithMessage("Timeslot does not exist in Database");

            //  Check is Date is valid and not already passed
            DateOnly resultDate;
            RuleFor(t => t.TimeSlot.Date).Must(data =>
            {
                return DateOnly.TryParse(data, out resultDate) && resultDate >= DateOnly.FromDateTime(DateTime.Today);
            }).WithMessage("Value is not a valid Date representation or date already passed!");
            //  Check if Time is valid
            TimeOnly resultTime;
            RuleFor(t => t.TimeSlot.Time).Must(data =>
            {
                return TimeOnly.TryParse(data, out resultTime);
            }).WithMessage("Value is not a valid Time representation!");
            //  Check of Date/Time combination is valid and not already passed
            DateTime resultDateTime;
            RuleFor(t => t.TimeSlot).Must(data =>
            {
                return DateTime.TryParse($"{data.Date} {data.Time}", out resultDateTime) && resultDateTime >= DateTime.Now;
            }).WithMessage("Value is not a valid DateTime representation or DateTime already passed!");

        }
    }
}
