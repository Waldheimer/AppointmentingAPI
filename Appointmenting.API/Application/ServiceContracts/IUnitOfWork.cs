namespace Appointmenting.API.Application.ServiceContracts
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
