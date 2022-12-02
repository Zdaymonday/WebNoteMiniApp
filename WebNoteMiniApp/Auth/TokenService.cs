using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebNoteMiniApp.Auth
{
    public class TokenService : ITokenService
    {
        private readonly TimeSpan Expire = TimeSpan.FromMinutes(30);

        public string BuildToken(string key, string issuer, UserDto userDto)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userDto.username),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            };

            var simKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(simKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.Add(Expire),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
