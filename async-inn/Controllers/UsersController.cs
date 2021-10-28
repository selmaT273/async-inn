using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using async_inn.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace async_inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterData data)
        {
            UserDTO user = await userService.Register(data, this.ModelState);
            if(user == null)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            return user;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginData data)
        {
            UserDTO user = await userService.Authenticate(data);

            if (user == null)
            {
                return Unauthorized();
            }

            return user;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<ActionResult<UserDTO>> Self()
        {
            return await userService.GetUser(this.User);
        }
    }
}