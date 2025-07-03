using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReparaStoreApp.Core.Services.Login;
using ReparaStoreApp.Data;
using ReparaStoreApp.Entities.Models.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Security.Security
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUserService _userService;

        public AuthService(JwtSettings jwtSettings, IUserService userService)
        {
            _jwtSettings = jwtSettings;
            _userService = userService;
        }

        public async Task<AuthResult> AuthenticateAsync(string username, string password)
        {
            var user = await _userService.AuthenticateUserAsync(username, password);

            if (user == null)
                return AuthResult.Fail("Credenciales inválidas");

            if (!user.IsActive)
                return AuthResult.Fail("Cuenta inactiva");

            var token = GenerateJwtToken(user);
            return AuthResult.Ok(token, user.Id);
            //jeffito
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            // Manejo seguro de roles
            if (user.UserRoles != null)
            {
                foreach (var userRole in user.UserRoles)
                {
                    if (userRole.Role?.Name != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
                    }
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
