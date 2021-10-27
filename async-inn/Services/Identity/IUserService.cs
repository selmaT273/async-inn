﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace async_inn.Services.Identity
{
    public interface IUserService
    {
        Task<UserDTO> Register(RegisterData data, ModelStateDictionary modelState);
        Task<UserDTO> Authenticate(LoginData data);
    }
}
