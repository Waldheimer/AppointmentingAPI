using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using Appointmenting.API.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            var result = ctx.TimeSlots.Where(t => t.day.Equals(date) && t.time.Equals(time));
            var error = result == null ? new Error("TimeSlot.NotFound", "Timeslot could not be found in the Database") : Error.None;
            Result<TimeSlot> res = new Result<TimeSlot>(result!.FirstOrDefault(), result != null, error);
            return Task.FromResult(res);
        }

        public Task<Result<IOrderedEnumerable<TimeSlot>?>> GetOrderedAscendingByDay(DateOnly date)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IOrderedEnumerable<TimeSlot>?>> GetOrderedAscendingFromDateOn(DateOnly date)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IOrderedEnumerable<TimeSlot>?>> GetOrderedAscendingFromDateToDate(DateOnly start, DateOnly end)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IOrderedEnumerable<TimeSlot>?>> GetOrderedAscendingFromTimeToTimeOnDate(DateOnly date, TimeOnly start, TimeOnly end)
        {
            throw new NotImplementedException();
        }

        //  **************************************************************
        //  ***** UPDATE  ************************************************
        //  **************************************************************
        public Task<Result<Guid>> Update(TimeSlot timeslot)
        {
            throw new NotImplementedException();
        }

        //  **************************************************************
        //  ***** DELETE  ************************************************
        //  **************************************************************
        public Task<Result<IEnumerable<Guid>>> DeleteByDate(DateOnly date)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Guid>>> DeleteByDateAndTime(DateOnly date, TimeOnly time)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Guid>>> DeleteByDateAndTimeSpan(DateOnly date, TimeOnly start, TimeOnly end)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Guid>> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        
        }

        
    }
}
