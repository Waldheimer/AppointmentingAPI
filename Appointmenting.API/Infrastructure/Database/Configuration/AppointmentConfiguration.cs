using Appointmenting.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointmenting.API.Infrastructure.Database.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            //  Sets and Converts the ID for the AppointmentType
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasConversion(
                apmntid => apmntid.Value,
                Value => new AppointmentId(Value));

        }
    }
}
