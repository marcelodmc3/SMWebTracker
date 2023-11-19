using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Domain.Utils
{
    public static class TokenGenerator
    {
        public static TokenModel GenerateUserToken(User user, string privateKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            byte[] chaveBytes = Convert.FromBase64String(privateKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Login.ToLower()),
                    new Claim(ClaimTypes.Role, user.IsAdmin.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenModel()
            {
                Token = tokenHandler.WriteToken(token),
                Type = JwtBearerDefaults.AuthenticationScheme,
                UserName = user.Name,
                UserEmail = user.Login,
                UserId = user.Id,
                IsAdmin = user.IsAdmin,
            };
        }
    }
}
