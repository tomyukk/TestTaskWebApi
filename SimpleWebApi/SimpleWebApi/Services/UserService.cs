using SimpleWebApi.Entities;
using SimpleWebApi.Interfaces;
using System.Security.Claims;

namespace SimpleWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashingService _hashingService;
        private readonly IJwtService _jwtService;

        public UserService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IHashingService hashingService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _hashingService = hashingService;
        }

        public async Task<User> GetById(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user;
        }

        public async Task CreateNewUser(string userName, string password)
        {
            var user = new User 
            {
                UserName = userName
            };

            user.PasswordHashSalt = _hashingService.GenerateSalt();
            user.PasswordHash = _hashingService.Hash(password, user.PasswordHashSalt);

            await _userRepository.AddAsync(user);
        }

        public async Task<bool> CheckCredentialsAsync(string userName, string password)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);
            if (user == null)
            {
                return false;
            }

            return user.PasswordHash == _hashingService.Hash(password, user.PasswordHashSalt);
        }

        public ClaimsIdentity GetClaimsIdentityAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim("Id",user.Id)
            };

            //return new ClaimsIdentity(claims, "Token");
            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

    }
}
