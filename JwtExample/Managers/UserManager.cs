using JwtExample.Context;
using JwtExample.Dtos;
using JwtExample.Entities;
using JwtExample.Services;
using JwtExample.Types;
using Microsoft.AspNetCore.Http.HttpResults;

namespace JwtExample.Managers
{
    public class UserManager : IUserService
    {
        private readonly JwtDbContext _db;

        public UserManager(JwtDbContext db)
        {
            _db = db;
        }

        public async Task<ServiceMassage> AddUser(RegisterDto registerDto)
        {
            var user = new UserEntity
            {
                Email = registerDto.Email,
                Password = registerDto.Password
            };

           await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return new ServiceMassage
            {
                Massage = "Kayit basarili",
                IsSucced = true,
            };

        }

        public async Task<ServiceMessage<UserInfoDto>> LoginUser(LoginDto user)
        {
            var result = _db.Users.Where(x => x.Email.ToLower() == user.Email.ToLower()).FirstOrDefault();

            if (result is null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed= false,
                    Message = "Giriş başarisiz kullanici adı veya şifre yanlış"
                };
            }

            if(user.Password == result.Password) 
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Message = "Giriş başarılı",
                    Data = new UserInfoDto
                    {
                        Id = result.Id,
                        Email = result.Email,
                        UserType = result.UserType,
                    }
                };
            }

            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Giriş başarisiz kullanici adı veya şifre yanlış"
                };
            }


        }
    }
}
