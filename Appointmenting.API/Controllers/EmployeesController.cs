using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.Queries;
using Appointmenting.API.Domain.DTOs;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using Appointmenting.API.Infrastructure.Validators.Employees;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Appointmenting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator mediator;

        public EmployeesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        //  **************************************************************
        //  ***** C R E A T E   ******************************************
        //  **************************************************************
        [HttpPost]
        public async Task<Result<EmployeeId>> CreateEmployee([FromBody] EmployeeDTO dto, CreateEmployeeValidator validator)
        {
            Employee employee = Employee.FromDTO(dto);
            var command = new CreateEmployeeCommand(employee);
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return new Result<EmployeeId>(EmployeeId.Empty, false,
                    new Error("EmployeeError.Creating", validationResult.Errors[0].ToString()));
            }
            return await mediator.Send(command);

        }

        //  **************************************************************
        //  ***** R E A D   **********************************************
        //  **************************************************************

        [HttpGet]
        public async Task<Result<List<Employee>>> GetAllEmployees()
        {
            var query = new GetAllEmployeesQuery();
            return await mediator.Send(query);
        }
        [HttpGet(":id")]
        public async Task<Result<Employee?>> GetEmployeeById(EmployeeId id)
        {
            var query = new GetEmployeeByIdQuery(id);
            return await mediator.Send(query);
        }

        //  **************************************************************
        //  ***** U P D A T E   ******************************************
        //  **************************************************************





        //  **************************************************************
        //  ***** D E L E T E   ******************************************
        //  **************************************************************
    }
}
