using JwtExample.Dtos;
using JwtExample.Jwt;
using JwtExample.RequestResponses;
using JwtExample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var userDto = new RegisterDto
            {
                Email = request.Email,
                Password = request.Password,
            };
            var result = await _userService.AddUser(userDto);

            if(result.IsSucced)
                return Ok(result.Massage);

            else return BadRequest(result.Massage);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var loginDto = new LoginDto
            {
                Email = request.Email,
                Password = request.Password,
            };

            var result = await _userService.LoginUser(loginDto);

            if(!result.IsSucceed)
                return BadRequest(result.Message);

            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwt(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)




            });

            return Ok(new LoginResponse
            {
                Message = "Giriş başarıyla tamamlandı.",
                Token = token

            });
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

    }
}
