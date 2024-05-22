using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointmenting.API.Infrastructure.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //  Convert FirstName => FirstName.Value
            //  Convert FirstName.Create(value).Value => value
            builder.Property(c => c.FirstName)
                .HasConversion(
                fn => fn.Value,
                value => FirstName.Create(value).Value);
            builder.Property(c => c.LastName)
                .HasConversion(
                fn => fn.Value,
                value => LastName.Create(value).Value);
        }
    }
}
