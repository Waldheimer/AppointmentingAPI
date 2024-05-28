using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointmenting.API.Infrastructure.Database.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId);

            builder.Property(e => e.EmployeeId)
                .HasConversion(
                employeeid => employeeid.value,
                value => new EmployeeId(value));

            builder.Property(c => c.FirstName)
                .HasConversion(
                fn => fn!.Value,
                value => FirstName.Create(value).Value);
            builder.Property(c => c.LastName)
                .HasConversion(
                fn => fn!.Value,
                value => LastName.Create(value).Value);
        }
    }
}
