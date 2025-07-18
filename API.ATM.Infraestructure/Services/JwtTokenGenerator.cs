using API.ATM.Application.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Infrastructure.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration Configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GenerateToken(string CardNumber, string Pin, string AccountNumber)
        {
            string? secretKey = Configuration["Jwt:Key"];
            string? issuer = Configuration["Jwt:Issuer"];
            string? audience = Configuration["Jwt:Audience"];

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(secretKey!));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            Claim[] claims =
            [
                new Claim("cardNumber", CardNumber),
                new Claim("pin", Pin),
                new Claim("accountNumber", AccountNumber)
            ];

            JwtSecurityToken token = new(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
