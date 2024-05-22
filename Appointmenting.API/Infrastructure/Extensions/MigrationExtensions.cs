using Appointmenting.API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Appointmenting.API.Infrastructure.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using AppUserDbContext? userContext = scope.ServiceProvider.GetService<AppUserDbContext>();
            using AppDbContext? dbContext = scope.ServiceProvider.GetService<AppDbContext>();

            try
            {
                userContext?.Database.Migrate();
                dbContext?.Database.Migrate();
            }
            catch { }
        }
    }
}
