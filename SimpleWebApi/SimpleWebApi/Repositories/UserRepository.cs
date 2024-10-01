using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Contexts;
using SimpleWebApi.Entities;
using SimpleWebApi.Interfaces;

namespace SimpleWebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SimpleApiContext _dbContext;

        public UserRepository(SimpleApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
