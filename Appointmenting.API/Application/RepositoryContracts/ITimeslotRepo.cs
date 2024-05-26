using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;

namespace Appointmenting.API.Application.RepositoryContracts
{
    public interface ITimeslotRepo
    {
        //  CREATE
        Task<Result<Guid>> Create(TimeslotDTO dto);

        //  READ
        //  ***** All Timeslots unordered
        Task<Result<IEnumerable<TimeSlot>?>> GetAll();
        //  ***** All Timeslots in ascending order by Date and Time
        Task<Result<List<TimeSlot>?>> GetAllOrderedAscending();
        //  ***** All Timeslots in ascending order by Date and Time on the given Date
        Task<Result<List<TimeSlot>?>> GetOrderedAscendingByDay(DateOnly date);
        //  ***** All Timeslots in ascending order by Date and Time from given Date on
        Task<Result<List<TimeSlot>?>> GetOrderedAscendingFromDateOn(DateOnly date);
        //  ***** All Timeslots in ascending order by Date and Time from StartDate to EndDate
        Task<Result<List<TimeSlot>?>> GetOrderedAscendingFromDateToDate(DateOnly start, DateOnly end);
        //  ***** All Timeslots in ascending order by Time from StartTime to EndTime on given Date
        Task<Result<List<TimeSlot>?>> GetOrderedAscendingFromTimeToTimeOnDate(DateOnly date, TimeOnly start, TimeOnly end);
        //  ***** The Timeslot with the given ID
        Task<Result<TimeSlot>> GetById(Guid id);
        //  ***** The Timeslot at given Date and Time
        Task<Result<TimeSlot?>> GetByDateAndTime(DateOnly date, TimeOnly time);

        //  UPDATE
        Task<Result<Guid>> Update(UpdateTimeslotDTO timeslot);

        //  DELETE
        Task<Result<Guid>> DeleteById(Guid id);
        //  ***** Delete all Timeslots on a given Date
        Task<Result<List<Guid>>> DeleteByDate(DateOnly date);
        //  ***** Delete all Timeslots on a given Date and a given Time
        Task<Result<List<Guid>>> DeleteByDateAndTime(DateOnly date, TimeOnly time);
        //  ***** Delete all Timeslots on a given Date from StartTime to EndTime
        Task<Result<List<Guid>>> DeleteByDateAndTimeSpan(DateOnly date, TimeOnly start, TimeOnly end);
    }
}
