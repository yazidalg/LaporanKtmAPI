using System;
using lapora_ktm_api.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace lapora_ktm_api.Config
{
	public class Jwt
	{
        private readonly IConfiguration _config;
        public Jwt(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJWTToken(Student user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(_config["Jwt:Key"])
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public int ValidateJwtToken(string? token)
        {
            if (token is null)
                return 0;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                return accountId;
            } catch
            {
                return 0;
            }
        }
    }
}

