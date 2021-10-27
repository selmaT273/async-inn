using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace async_inn.Services.Identity
{
    public class IdentityUserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public IdentityUserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ApplicationUser> Register(RegisterData data)
        {
            ApplicationUser user = new ApplicationUser
            {
                Email = data.Email,
                UserName = data.Username
            };
            
            IdentityResult result = await userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                return user;
            }

            return null;
        }
    }
}
