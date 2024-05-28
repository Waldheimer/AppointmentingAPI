using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using Appointmenting.API.Domain.ValueObjects;

namespace Appointmenting.API.Application.RepositoryContracts
{
    public interface IEmployeeRepo
    {
        //  C R E A T E
        Task<Result<EmployeeId>> CreateEmployee(Employee employee);
        //  R E A D
        Task<Result<Employee>> GetEmployeeById(EmployeeId employeeId);
        Task<Result<List<Employee>>> GetAllEmployees();
        //  U P D A T E
        Task<Result<EmployeeId>> UpdateEmployee(Employee employee);
        //  D E L E T E
        Task<Result<EmployeeId>> DeleteEmployeeById(EmployeeId employeeId);
        Task<Result<EmployeeId>> DeleteEmployeeByNames(FirstName firstName, LastName lastName);
    }
}
