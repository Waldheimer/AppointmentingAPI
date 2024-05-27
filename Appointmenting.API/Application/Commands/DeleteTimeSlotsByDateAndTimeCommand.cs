using Appointmenting.API.Domain.Primitives;
using MediatR;

namespace Appointmenting.API.Application.Commands
{
    public class DeleteTimeSlotsByDateAndTimeCommand : IRequest<Result<Guid>>
    {
        public string Date { get; set; }
        public string Time { get; set; }

        public DeleteTimeSlotsByDateAndTimeCommand(string date, string time)
        {
            Date = date;
            Time = time;
        }
    }
}
