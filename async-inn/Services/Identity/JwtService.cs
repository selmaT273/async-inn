using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace async_inn.Services.Identity
{
    public class JwtService
    {
        private readonly SignInManager <ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public JwtService(SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<string> GetToken(ApplicationUser user, TimeSpan timeSpan)
        {
            ClaimsPrincipal principal = await signInManager.CreateUserPrincipalAsync(user);
            if (principal == null)
            {
                return null;
            }

            SecurityKey signingKey = GetSecurityKey(configuration);
            JwtSecurityToken token = new JwtSecurityToken(
                expires: DateTime.UtcNow + timeSpan,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                claims: principal.Claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static TokenValidationParameters GetValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(configuration),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        }

        private static SecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secret = configuration["JWT:Secret"];
            if (secret == null)
            { 
                throw new InvalidOperationException("JWT:Secret is missing");
            }
            byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
            return new SymmetricSecurityKey(secretBytes);
        }

    }
}
