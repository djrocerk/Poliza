using Domain.Entities;
using Domain.Models;
using Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Jwt
{
    public interface IJwtFactory
    {
        LoginResponse CreateJwtToken(Usuario user);
    }

    public class JwtFactory : IJwtFactory
    {
        private readonly JwtSettings _jwtSettings;

        public JwtFactory(IOptions<JwtSettings> jwtSettings)
            => _jwtSettings = jwtSettings.Value;

        public LoginResponse CreateJwtToken(Usuario user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user!.Id!, ClaimValueTypes.String, _jwtSettings.Issuer),
                new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String, _jwtSettings.Issuer),
                new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.String, _jwtSettings.Issuer)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                NotBefore = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return new LoginResponse
            {
                AccessToken = token,
                ExpiresAt = expires
            };
        }
    }
}
