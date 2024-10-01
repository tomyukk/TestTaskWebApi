using SimpleWebApi.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace SimpleWebApi.Services
{
    public class HashingService : IHashingService
    {
        private readonly int ConstSaltLength = 8;
        public string GenerateSalt()
        {
            var saltBytes = RandomNumberGenerator.GetBytes(ConstSaltLength);
            return Convert.ToBase64String(saltBytes);
        }

        public string Hash(string valueToHash, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var combined = Encoding.UTF8.GetBytes(valueToHash + salt);
                var hashBytes = sha256.ComputeHash(combined);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
