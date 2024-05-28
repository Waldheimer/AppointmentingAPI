using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Domain.Primitives;
using Appointmenting.API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Appointmenting.API.Infrastructure.Repositories
{
    public class UserRepository : IUserRepo
    {
        private readonly AppUserDbContext ctx;

        public UserRepository(AppUserDbContext ctx)
        {
            this.ctx = ctx;
        }

        public Task<Result<User>> GetUserById(string id)
        {
            Result<User> res;
            var result = ctx.Users.AsNoTracking().Where(x => x.Id == id).First();
            if(result == null)
            {
                res = new(null, false, new Error("UserError.NotFound", "No User with given id could be found"));
            }
            else { 
                res = new(result, true, Error.None);
            }
            return Task.FromResult(res);
        }
        public Task<Result<User>> GetUserByEmail(string email)
        {
            Result<User> res;
            var result = ctx.Users.AsNoTracking().Where(x => x.Email!.Equals(email)).First();
            if (result == null)
            {
                res = new(null, false, new Error("UserError.NotFound", "No User with given email could be found"));
            }
            else
            {
                res = new(result, true, Error.None);
            }
            return Task.FromResult(res);
        }
        public Task<Result<IEnumerable<User>>> GetAllUsers()
        {
            Result<IEnumerable<User>> res;
            var result = ctx.Users.AsNoTracking().AsEnumerable();
            if(result == null)
            {
                res = new(null, false, new Error("UserError.NotFound", "No User could be found"));
            }
            else
            {
                res = new(result, true, Error.None);
            }
            return Task.FromResult(res);
        }
    }
}
