using JwtExample.Dtos;
using JwtExample.Types;

namespace JwtExample.Services
{
    public interface IUserService
    {
        Task<ServiceMassage> AddUser(RegisterDto registerDto);

        Task<ServiceMessage<UserInfoDto>> LoginUser(LoginDto user);

    }
}
