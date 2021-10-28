using System;
using System.Collections.Generic;

namespace async_inn.Services.Identity
{
    public class UserDTO
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }

        public IList<string> Roles { get; set; }

        public string Token { get; set; }
    }
}
