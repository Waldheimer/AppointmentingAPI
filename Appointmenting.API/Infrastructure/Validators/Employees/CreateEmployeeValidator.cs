using Appointmenting.API.Application.Commands;
using Appointmenting.API.Application.RepositoryContracts;
using FluentValidation;

namespace Appointmenting.API.Infrastructure.Validators.Employees
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeValidator(IEmployeeRepo repo)
        {
            RuleFor(e => e.Employee.FirstName).NotEmpty().WithMessage("FirstName is required!");
            RuleFor(e => e.Employee.LastName).NotEmpty().WithMessage("LastName is required!");
            RuleFor(e => e.Employee).NotEmpty().Must(data =>
            {
                var exists = repo.GetEmployeeById(data.EmployeeId);
                return exists != null;
            }).WithMessage("Employee already exists");
        }
    }
}
