using SimpleWebApi.Entities;

namespace SimpleWebApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);
        Task<User> GetByUserNameAsync(string userName);
        Task AddAsync(User user);
    }
}
