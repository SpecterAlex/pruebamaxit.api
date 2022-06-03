using Employees.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<AccessToken> TokenGenerate(User user)
        {
            //Creamos nuestra lista de Claims, en este caso para el Username,
            //el Email
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.UserId.ToString())
            };

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var date = DateTime.UtcNow;

            var expires = date.AddDays(3);

            // Creamos el objeto JwtSecurityToken
            var token = new JwtSecurityToken(
              issuer: _configuration["Jwt:Issuer"],
              audience: _configuration["Jwt:Audience"],
              claims: claims,
              notBefore: date,
              expires: expires,
              signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new AccessToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expires.ToString(),
            };
        }
    }
}
