using Appointmenting.API.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Appointmenting.API.Infrastructure.Database
{
    public class AppUserDbContext : IdentityDbContext<User>
    {
        public AppUserDbContext(DbContextOptions<AppUserDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //  Tell the DbContextBuilder to build especially for Sqlite
            optionsBuilder.UseSqlite();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Use the *UserConfiguration* to Convert the stronglyTyped FirstName/LastName
            //  to DB usable values and back
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppUserDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
