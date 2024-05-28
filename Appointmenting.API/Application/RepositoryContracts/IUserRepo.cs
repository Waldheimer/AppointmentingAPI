using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;

namespace Appointmenting.API.Application.RepositoryContracts
{
    public interface IUserRepo
    {
        Task<Result<User>> GetUserById(string id);
        Task<Result<User>> GetUserByEmail(string email);
        Task<Result<IEnumerable<User>>> GetAllUsers();
    }
}
