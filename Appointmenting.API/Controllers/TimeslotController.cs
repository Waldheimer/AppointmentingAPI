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
        //  ***********************************************
        //  ***** CREATE    *******************************
        //  ***********************************************
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
        //  ***********************************************
        //  ***** READ  ***********************************
        //  ***********************************************
        [HttpGet]
        public async Task<Result<List<TimeSlot>?>> GetAll()
        {
            var query = new GetAllTimeSlotsQuery();
            var result = await mediator.Send(query);

            return result;
        }

        //  ***********************************************
        //  ***** UPDATE  *********************************
        //  ***********************************************




        //  ***********************************************
        //  ***** DELETE  *********************************
        //  ***********************************************
    }
}
