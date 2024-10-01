using Microsoft.IdentityModel.Tokens;
using SimpleWebApi.Interfaces;
using SimpleWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SimpleWebApi.Services
{
    public class JwtService : IJwtService
    {
        public string GenerateToken(ClaimsIdentity identity)
        {
            DateTime now = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                            claims: identity.Claims,
                            expires: now.Add(TimeSpan.FromSeconds(AuthOptions.LifeTimeSeconds)),
                            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public string ValidateToken(string token)
        {
            if (token == null)
                return string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "Id").Value;

                return userId;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
