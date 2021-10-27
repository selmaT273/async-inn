using System;
using System.Threading.Tasks;

namespace async_inn.Services.Identity
{
    public interface IUserService
    {
        Task Register(RegisterData data);
    }
}
