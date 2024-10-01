using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SimpleWebApi.Models
{
    public static class AuthOptions
    {
        private const string Key = "Gi6awqTp07RCWK5k7Miaadfwddfwioolwd";
        public const int LifeTimeSeconds = 60 * 60 * 24;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
