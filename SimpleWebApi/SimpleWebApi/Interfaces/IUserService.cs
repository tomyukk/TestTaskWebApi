using SimpleWebApi.Entities;
using System.Security.Claims;

namespace SimpleWebApi.Interfaces
{
    public interface IUserService
    {
        Task<User> GetById(string id);
        Task<bool> CheckCredentialsAsync(string userName, string password);
        Task CreateNewUser(string userName, string password);
        ClaimsIdentity GetClaimsIdentityAsync(User user);
    }
}
