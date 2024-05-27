using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using FluentValidation;

namespace Appointmenting.API.Infrastructure.Validators
{
    public class DeleteTimeslotByIdValidator : AbstractValidator<DeleteTimeSlotByIDCommand>
    {
        public DeleteTimeslotByIdValidator(ITimeslotRepo repo) 
        {
            RuleFor(t => t.ID).NotEmpty().Must(data =>
            {
                var result = repo.GetById(data);
                return result != null;
            }).WithMessage("Given ID can not be found in Database");
        }
    }
    public class DeleteTimeslotsByDateValidator : AbstractValidator<DeleteTimeSlotsByDateCommand>
    {
        public DeleteTimeslotsByDateValidator(ITimeslotRepo repo)
        {
            RuleFor(t => t.Date).NotEmpty().Must(data =>
            {
                var result = repo.GetOrderedAscendingByDay(data);
                return result != null;
            }).WithMessage("No Timeslots given Date");
        }
    }
    public class DeleteTimeslotsBeforeDateValidator : AbstractValidator<DeleteTimeSlotsBeforeDateCommand>
    {
        public DeleteTimeslotsBeforeDateValidator(ITimeslotRepo repo)
        {
            RuleFor(t => t.Date).NotEmpty().Must(data =>
            {
                var result = repo.DeleteBeforeDate(data);
                return result != null;
            }).WithMessage("Not Timeslots before given Date");
        }
    }
}
