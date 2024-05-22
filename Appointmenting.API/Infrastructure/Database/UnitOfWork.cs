using Appointmenting.API.Application.ServiceContracts;

namespace Appointmenting.API.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext ctx;

        public UnitOfWork(AppDbContext ctx)
        {
            this.ctx = ctx;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default) 
        { 
            return ctx.SaveChangesAsync(cancellationToken);
        }

    }
}
