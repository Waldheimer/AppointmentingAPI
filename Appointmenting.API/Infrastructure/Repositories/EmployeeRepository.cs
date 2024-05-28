using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using Appointmenting.API.Domain.ValueObjects;
using Appointmenting.API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Appointmenting.API.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepo
    {
        private readonly AppDbContext ctx;

        public EmployeeRepository(AppDbContext ctx)
        {
            this.ctx = ctx;
        }

        //  **************************************************************
        //  ***** C R E A T E   ******************************************
        //  **************************************************************
        public Task<Result<EmployeeId>> CreateEmployee(Employee employee)
        {
            Result<EmployeeId> res;
            var result = ctx.Employees.Add(employee);
            if(result == null) 
            {
                res = new(EmployeeId.Empty, false, new Error("EmployeeError.Creation", "Employee could not be created"));
            }
            else
            {
                res = new(result.Entity.EmployeeId, true, Error.None);
            }
            return Task.FromResult(res);
        }

        //  **************************************************************
        //  ***** R E A D   **********************************************
        //  **************************************************************
        public Task<Result<Employee>> GetEmployeeById(EmployeeId employeeId)
        {
            Result<Employee> res;
            var result = ctx.Employees.AsNoTracking().FirstOrDefault(e =>  e.EmployeeId == employeeId);
            if(result == null)
            {
                res = new(null, false, new Error("EmployeeError.NotFound", "Employee with given Id could not be found"));
            }
            else
            {
                res = new(result, true, Error.None);
            }
            return Task.FromResult(res);
        }
        
        public Task<Result<List<Employee>>> GetAllEmployees()
        {
            Result<List<Employee>> res;
            var result = ctx.Employees.AsNoTracking().ToList();
            if(result == null)
            {
                res = new(new List<Employee>(), false, new Error("EmployeeError.NotFound", "No Employees could be found"));
            }
            else
            {
                res = new(result,true, Error.None);
            }
            return Task.FromResult(res); 
        }

        //  **************************************************************
        //  ***** U P D A T E   ******************************************
        //  **************************************************************
        public Task<Result<EmployeeId>> UpdateEmployee(Employee employee)
        {
            Result<EmployeeId> res;
            var result = ctx.Employees.Update(employee);
            if(result == null)
            {
                res = new(EmployeeId.Empty, false, new Error("EmployeeError.Update", "Employee could not be updated"));
            }
            else
            {
                res = new(result.Entity.EmployeeId, true, Error.None);
            }
            return Task.FromResult(res);
        }

        //  **************************************************************
        //  ***** D E L E T E   ******************************************
        //  **************************************************************

        public Task<Result<EmployeeId>> DeleteEmployeeById(EmployeeId employeeId)
        {
            Result<EmployeeId> res;
            var result = ctx.Employees.Remove(ctx.Employees.AsNoTracking().First(c => c.EmployeeId == employeeId));
            if(result == null)
            {
                res = new(EmployeeId.Empty, false, new Error("EmployeeError.Delete", "Employee could not be deleted"));
            }
            else
            {
                res = new(result.Entity.EmployeeId, true, Error.None);
            }
            return Task.FromResult(res);
        }

        public Task<Result<EmployeeId>> DeleteEmployeeByNames(FirstName firstName, LastName lastName)
        {
            Result<EmployeeId> res;
            var result = ctx.Employees.Remove(ctx.Employees.AsNoTracking().First(c => c.FirstName == firstName && c.LastName == lastName));
            if(result == null)
            {
                res = new(EmployeeId.Empty, false, new Error("EmployeeError.Delete", "Employee could not be deleted"));
            }
            else
            {
                res = new(result.Entity.EmployeeId, true, Error.None);
            }
            return Task.FromResult(res);
        }


    }
}
