using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace async_inn.Services.Identity
{
    public class IdentityUserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtService jwtService;

        public IdentityUserService(UserManager<ApplicationUser> userManager, JwtService jwtService)
        {
            this.userManager = userManager;
            this.jwtService = jwtService;
        }

        public async Task<UserDTO> Authenticate(LoginData data)
        {
            ApplicationUser user = await userManager.FindByNameAsync(data.Username);

            if(!await userManager.CheckPasswordAsync(user, data.Password))
            {
                return null;
            }

            return new UserDTO
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
            };
        }

        public async Task<UserDTO> Register(RegisterData data, ModelStateDictionary modelState)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = data.Email,
                UserName = data.Username
            };
            
            IdentityResult result = await userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                return new UserDTO
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Username = user.UserName,
                };
            }

            foreach (IdentityError error in result.Errors)
            {
                string errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                    "";
                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }
    }
}
