using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using Appointmenting.API.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appointmenting.API.Infrastructure.Repositories
{
    public class TimeSlotRepository : ITimeslotRepo
    {
        private readonly AppDbContext ctx;

        public TimeSlotRepository(AppDbContext ctx)
        {
            this.ctx = ctx;
        }
        //  **************************************************************
        //  ***** CREATE    **********************************************
        //  **************************************************************
        public Task<Result<Guid>> Create(TimeslotDTO dto)
        {
            var result = ctx.TimeSlots.Add(TimeSlot.FromDTO(dto));
            Result<Guid> res;
            if(result != null)
            {
                res = new Result<Guid>(result.Entity.Id, true, Error.None);
            }
            else
            {
                res = new Result<Guid>(Guid.Empty, false, new Error("TimeSlot.Create", "Error creating a new TimeSlot"));
            }

            return Task.FromResult(res);
        }

        //  **************************************************************
        //  ***** READ  **************************************************
        //  **************************************************************
        public Task<Result<IEnumerable<TimeSlot>?>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<TimeSlot>?>> GetAllOrderedAscending()
        {
            var result = ctx.TimeSlots.OrderBy(t => t.day).OrderBy(t => t.time).AsNoTracking().ToList();
            Result<List<TimeSlot>?> res;
            if (result.Count() > 0)
            {
                res = new(result, true, Error.None);
            }
            else
            {
                res = new(null, false, new Error("TimeSlot.Read", "Unable to find any TimeSlots"));
            }
            return Task.FromResult(res);
        }

        public Task<Result<TimeSlot>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<TimeSlot>> GetByDateAndTime(DateOnly date, TimeOnly time)
        {
            var result = ctx.TimeSlots.Where(t => t.day.Equals(date) && t.time.Equals(time)).FirstOrDefault();
            var error = 
                result == null 
                ? new Error("TimeSlot.NotFound", "Timeslot could not be found in the Database") 
                : Error.None;
            Result<TimeSlot> res = new Result<TimeSlot>(result, result != null, error);
            return Task.FromResult(res);
        }

        public Task<Result<List<TimeSlot>?>> GetOrderedAscendingByDay(DateOnly date)
        {
            var result = ctx.TimeSlots.Where(t => t.day.Equals(date)).OrderBy(t => t.time).ToList();
            var error =
                result == null
                ? new Error("TimeSlot.NotFound", "Timeslots for given Date could not be found in the Database")
                : Error.None;
            Result<List<TimeSlot>?> res = new Result<List<TimeSlot>?>(result, result != null, error);
            return Task.FromResult(res);
        }

        public Task<Result<List<TimeSlot>?>> GetOrderedAscendingFromDateOn(DateOnly date)
        {
            var result = ctx.TimeSlots.Where(t => t.day >= date).OrderBy(t => t.time).ToList();
            var error =
                result == null
                ? new Error("TimeSlot.NotFound", "Timeslots from given Date on could not be found in the Database")
                : Error.None;
            Result<List<TimeSlot>?> res = new Result<List<TimeSlot>?>(result, result != null, error);
            return Task.FromResult(res);
        }

        public Task<Result<List<TimeSlot>?>> GetOrderedAscendingFromDateToDate(DateOnly start, DateOnly end)
        {
            var result = ctx.TimeSlots.Where(t => t.day >= start && t.day <= end).OrderBy(t => t.time).ToList();
            var error =
                result == null
                ? new Error("TimeSlot.NotFound", "Timeslots between given Dates could not be found in the Database")
                : Error.None;
            Result<List<TimeSlot>?> res = new Result<List<TimeSlot>?>(result, result != null, error);
            return Task.FromResult(res);
        }

        public Task<Result<List<TimeSlot>?>> GetOrderedAscendingFromTimeToTimeOnDate(DateOnly date, TimeOnly start, TimeOnly end)
        {
            var result = ctx.TimeSlots.Where(t => t.day.Equals(date) && t.time >= start && t.time <= end).OrderBy(t => t.time).ToList();
            var error =
                result == null
                ? new Error("TimeSlot.NotFound", "Timeslots from given Times on given Date could not be found in the Database")
                : Error.None;
            Result<List<TimeSlot>?> res = new Result<List<TimeSlot>?>(result, result != null, error);
            return Task.FromResult(res);
        }

        //  **************************************************************
        //  ***** UPDATE  ************************************************
        //  **************************************************************
        public Task<Result<Guid>> Update(TimeSlot timeslot)
        {
            var result = ctx.TimeSlots.FirstOrDefault(t => t.Id == timeslot.Id);
            var error =
                result == null
                ? new Error("TimeSlot.NotFound", "Timeslot with given ID could not be found in the Database")
                : Error.None;
            if(result != null)
            {
                ctx.TimeSlots.Add(timeslot);
                ctx.TimeSlots.Remove(result);
            }
            Result<Guid> res = new Result<Guid>(timeslot.Id, result != null, error);
            return Task.FromResult(res);
        }

        //  **************************************************************
        //  ***** DELETE  ************************************************
        //  **************************************************************
        public Task<Result<IEnumerable<Guid>>> DeleteByDate(DateOnly date)
        {
            var deletedGuids = new List<Guid>();
            var result = ctx.TimeSlots.Where(t => t.day.Equals(date)).ToList();
            var error =
                result == null
                ? new Error("TimeSlot.NotFound", "Timeslots on given Date could not be found in the Database")
                : Error.None;
            if(result != null && result.Count > 0)
            {
                foreach( var t in result)
                {
                    ctx.TimeSlots.Remove(t);
                    deletedGuids.Add(t.Id);
                }
            }
            Result<IEnumerable<Guid>> res = new Result<IEnumerable<Guid>>(deletedGuids, deletedGuids.Count > 0, error);
            return Task.FromResult(res);
        }

        public Task<Result<IEnumerable<Guid>>> DeleteByDateAndTime(DateOnly date, TimeOnly time)
        {
            var deletedGuids = new List<Guid>();
            var result = ctx.TimeSlots.Where(t => t.day.Equals(date) && t.time.Equals(time)).ToList();
            var error =
                result == null
                ? new Error("TimeSlot.NotFound", "Timeslots on given Date and Time could not be found in the Database")
                : Error.None;
            if (result != null && result.Count > 0)
            {
                foreach (var t in result)
                {
                    ctx.TimeSlots.Remove(t);
                    deletedGuids.Add(t.Id);
                }
            }
            Result<IEnumerable<Guid>> res = new Result<IEnumerable<Guid>>(deletedGuids, deletedGuids.Count > 0, error);
            return Task.FromResult(res);
        }

        public Task<Result<IEnumerable<Guid>>> DeleteByDateAndTimeSpan(DateOnly date, TimeOnly start, TimeOnly end)
        {
            var deletedGuids = new List<Guid>();
            var result = ctx.TimeSlots.Where(t => t.day.Equals(date) && t.time >= start && t.time <= end).ToList();
            var error =
                result == null
                ? new Error("TimeSlot.NotFound", "Timeslots on given Date and Times could not be found in the Database")
                : Error.None;
            if (result != null && result.Count > 0)
            {
                foreach (var t in result)
                {
                    ctx.TimeSlots.Remove(t);
                    deletedGuids.Add(t.Id);
                }
            }
            Result<IEnumerable<Guid>> res = new Result<IEnumerable<Guid>>(deletedGuids, deletedGuids.Count > 0, error);
            return Task.FromResult(res);
        }

        public Task<Result<Guid>> DeleteById(Guid id)
        {
            var result = ctx.TimeSlots.Where(t => t.Id.Equals(id)).FirstOrDefault();
            var error =
                result == null
                ? new Error("TimeSlot.NotFound", "Timeslots with given ID could not be found in the Database")
                : Error.None;
            if (result != null)
            {
                ctx.TimeSlots.Remove(result);
            }
            Result<Guid> res = new Result<Guid>(id, result != null, error);
            return Task.FromResult(res);
        }

        
    }
}
