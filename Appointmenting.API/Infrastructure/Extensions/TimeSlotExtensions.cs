using Appointmenting.API.Domain.Entities;

namespace Appointmenting.API.Infrastructure.Extensions
{
    public static class TimeSlotExtensions
    {
        public static TimeSlot WithEmptyId(this TimeSlot timeslot)
        {
            timeslot.Id = Guid.Empty;
            return timeslot;
        }
    }
}
