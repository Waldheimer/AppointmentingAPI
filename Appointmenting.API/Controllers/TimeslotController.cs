using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.Queries;
using Appointmenting.API.Application.ServiceContracts;
using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using Appointmenting.API.Infrastructure.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointmenting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeslotController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUnitOfWork unit;

        public TimeslotController(IMediator mediator, IUnitOfWork unit)
        {
            this.mediator = mediator;
            this.unit = unit;
        }
        //  *******************************
        //  ***** C R E A T E   ***********
        //  *******************************

        //  ----- URL       : api/timeslot                      -------------------------
        //  ----- POST      : given TimeSlot as TimeSlotDTO     -------------------------
        //  ----- Validates : the dto's data against DateOnly/TimeOnly, -----------------
        //                  : passed Time and duplicate Database entry  -----------------
        [HttpPost]
        public async Task<Result<Guid>> CreateNewTimeSlot([FromBody] TimeslotDTO dto, CreateTimeSlotValidator validator)
        {
            var command = new CreateTimeSlotCommand(dto);
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return new Result<Guid>(Guid.Empty, false, new Error("Parsing.Error", validationResult.Errors[0].ToString()));
            }
            var result = await mediator.Send(command);
            if (result != null)
            {
                await unit.SaveChangesAsync(CancellationToken.None);
            }
            return result!;
        }
        
        //  *******************************
        //  ***** R E A D   ***************
        //  *******************************

        //  ----- URL       : api/timeslot      -----------------------------------------
        //  ----- GET       : all TimeSlots     -----------------------------------------
        //  ----- Validates : *****             ----------------------------------------- 
        [HttpGet]
        public async Task<Result<List<TimeSlot>?>> GetAll()
        {
            var query = new GetAllTimeSlotsQuery();
            var result = await mediator.Send(query);

            return result;
        }

        //  ----- URL       : api/timeslot/date                     ---------------------
        //  ----- GET       : all TimeSlots by given Date           ---------------------
        //  ----- Validates : argument against DateOnly/TimeOnly    ---------------------
        [HttpGet("date")]
        public async Task<Result<List<TimeSlot>?>> GetAllByDate(string date, DateTimeValidator validator)
        {
            ConversionResult validationResult = validator.TryValidate(date);
            if(!validationResult.DateSuccess) 
            {
                return new Result<List<TimeSlot>?>(null, false, 
                    new Error("DateError.Parsing", "Value is not a valid Date/Time representation"));
            }
            else
            {
                var query = new GetTimeSlotsByDateQuery(validationResult.Day);
                var result = await mediator.Send(query);
                return result;
            }
        }

        //  ----- URL       : api/timeslot/date/time                ---------------------
        //  ----- GET       : TimeSlot by Date and Time             ---------------------
        //  ----- Validates : arguments against DateOnly/TimeOnly   ---------------------
        [HttpGet("date/time")]
        public async Task<Result<TimeSlot?>> GetByDateAndTime(string date, string time, DateTimeValidator validator)
        {
            var validationResult = validator.TryValidate(date, time);
            if (!(validationResult.DateSuccess & validationResult.TimeSuccess))
            {
                return new Result<TimeSlot?>(null, false, 
                    new Error("DateTimeError.Parsing", "At least one of the given values is not a valid Date/Time representation"));
            }
            else
            {
                var query = new GetTimeSlotsByDateTimeQuery(validationResult.Day, validationResult.Time);
                var result = await mediator.Send(query);
                return result;
            }
        }

        //  ----- URL       : api/timeslot/startdate                ---------------------
        //  ----- GET       : all TimeSlots from the given Date on  ---------------------
        //  ----- Validates : argument against DateOnly             ---------------------
        [HttpGet("startdate")]
        public async Task<Result<List<TimeSlot>?>> GetFromDateOn(string date, DateTimeValidator validator)
        {
            var validationResult = validator.TryValidate(date);
            if (!validationResult.DateSuccess)
            {
                return new Result<List<TimeSlot>?>(null, false,
                    new Error("DateError.Parsing", "Value is not a valid Date representation!"));
            }
            else
            {
                var query = new GetTimeSlotsFromDateOnQuery(validationResult.Day);
                var result = await mediator.Send(query);
                return result;
            }
        }

        //  ----- URL       : api/timeslot/startdate/enddate       ----------------------
        //  ----- GET       : all TimeSlots between StartDate and EndDate   -------------
        //  ----- Validates : arguments against DateOnly            ---------------------
        [HttpGet("startdate/enddate")]
        public async Task<Result<List<TimeSlot>?>> GetFromDateToDate(string start, string end, DateTimeValidator validator)
        {
            var validationResult_start = validator.TryValidate(start);
            var validationResult_end = validator.TryValidate(end);
            if(!(validationResult_start.DateSuccess && validationResult_end.DateSuccess))
            {
                return new(null, false, new Error("DateError.Parsing", "At least one value is not a valid Date representation!"));
            }
            else
            {
                var query = new GetTimeSlotsFromDateToDateQuery(validationResult_start.Day, validationResult_end.Day);
                var result = await mediator.Send(query);
                return result;
            }
        }

        //  ----- URL       : api/timeslot/day/starttime/endtime    ---------------------
        //  ----- GET       : all TimeSlots on given Day between StartTime and EndTime --
        //  ----- Validates : arguments against DateOnly/TimeOnly
        [HttpGet("date/starttime/endtime")]
        public async Task<Result<List<TimeSlot>?>> GetFromTimeToTimeOnDate(string date, string start, string end, 
            DateTimeValidator validator)
        {
            var validationResult_datestart = validator.TryValidate(date, start);
            var validationResult_end = validator.TryValidate(end);
            if (!(validationResult_datestart.DateSuccess 
                && validationResult_datestart.TimeSuccess 
                && validationResult_end.TimeSuccess))
            {
                return new(null, false, new Error("DateTimeError.Parsing", "At least on value is not a valid Date/Time representation"));
            }
            else
            {
                var query = new GetTimeSlotsFromTimeToTimeOnDateQuery(validationResult_datestart.Day,
                    validationResult_datestart.Time, validationResult_end.Time);
                var result = await mediator.Send(query);
                return result;
            }
        }

        //  *********************************
        //  ***** U P D A T E   *************
        //  *********************************

        //  ----- URL       : api/timeslot                              -----------------
        //  ----- PUT       : the TimeSlot to update with new values    -----------------
        //  ----- Validates : the argument against existence in Database     ------------
        [HttpPut]
        public async Task<Result<Guid>> Update([FromBody] UpdateTimeslotDTO timeSlot, UpdateTimeslotValidator validator)
        {
            var command = new UpdateTimeSlotCommand(timeSlot);
            var validationResult =  validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return new Result<Guid>(Guid.Empty, false, new Error("Parsing.Error", validationResult.Errors[0].ToString()));
            }
            return await mediator.Send(command);
        }

        //  *********************************
        //  ***** D E L E T E   *************
        //  *********************************

        //  ----- URL       : api/timeslot/:id                          -----------------
        //  ----- DELETE    : the TimeSlot with the given id            -----------------
        //  ----- Validates : the argument against existence in Database     ------------
        [HttpDelete(":id")]
        public async Task<Result<Guid>> DeleteById(Guid id, DeleteTimeslotByIdValidator validator)
        {
            var command = new DeleteTimeSlotByIDCommand(id);
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return new Result<Guid>(Guid.Empty, false, 
                    new Error("TimeslotError.NotFound", validationResult.Errors[0].ToString()));
            }
            return await mediator.Send(command);
        }

        //  ----- URL       : api/timeslot/date                         -----------------
        //  ----- DELETE    : the TimeSlots on the given date           -----------------
        //  ----- Validates : the argument against DateOnly             -----------------
        //                  : the existence of TimeSlots on the given date  -------------
        [HttpDelete("date")]
        public async Task<Result<List<Guid>>> DeleteByDate(string date, DateTimeValidator dtval,
            DeleteTimeslotsByDateValidator validator)
        {
            var dateValidationResult = dtval.TryValidate(date);
            if(!dateValidationResult.DateSuccess) 
            {
                return new(null, false, new Error("Parsing.Error", "Value is not a valid Date representation"));
            }
            var command = new DeleteTimeSlotsByDateCommand(dateValidationResult.Day);
            var validationResult = validator.Validate(command);
            if(!validationResult.IsValid)
            {
                return new(null, false, new Error("TimeslotError.NotFound", validationResult.Errors[0].ToString()));
            }
            return await mediator.Send(command);
        }

        //  ----- URL       : api/timeslot/allbefore/date               -----------------
        //  ----- DELETE    : the TimeSlots before the given date       -----------------
        //  ----- Validates : the argument against DateOnly             -----------------
        //                  : the existence of TimeSlots before the given date  ---------
        [HttpDelete("allbefore/date")]
        public async Task<Result<List<Guid>>> DeleteBeforeDate(string date, DateTimeValidator dtval,
            DeleteTimeslotsBeforeDateValidator validator)
        {
            var dateValidationResult = dtval.TryValidate(date);
            if(!dateValidationResult.DateSuccess)
            {
                return new(null, false, new Error("Parsing.Error", "Value is not a valid Date representation"));
            }
            var command = new DeleteTimeSlotsBeforeDateCommand(dateValidationResult.Day);
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return new(null, false, new Error("TimeslotError.NotFound", validationResult.Errors[0].ToString()));
            }
            return await mediator.Send(command);
        }

    }
}
