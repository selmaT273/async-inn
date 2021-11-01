using System;
using System.Security.Claims;
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

            return await GenerateUserDTO(user);

        }

        private async Task<UserDTO> GenerateUserDTO(ApplicationUser user)
        {
            return new UserDTO
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Roles = await userManager.GetRolesAsync(user),
                Token = await jwtService.GetToken(user, TimeSpan.FromMinutes(10))
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
                if(data.Roles.Length > 0)
                {
                    await userManager.AddToRolesAsync(user, data.Roles);
                }

                return await GenerateUserDTO(user);
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

        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            ApplicationUser user = await userManager.GetUserAsync(principal);
            if(user == null)
            {
                return null;
            }

            return await GenerateUserDTO(user);
        }
    }
}
