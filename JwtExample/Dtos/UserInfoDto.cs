using JwtExample.Entities;
using JwtExample.Types;

namespace JwtExample.Dtos
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        
        public UserType UserType { get; set; }
    }
}
