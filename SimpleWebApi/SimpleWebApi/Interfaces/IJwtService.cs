using System.Security.Claims;

namespace SimpleWebApi.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(ClaimsIdentity identity);
        string ValidateToken(string token);
    }
}
