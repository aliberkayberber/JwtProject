using System.ComponentModel.DataAnnotations;

namespace JwtExample.RequestResponses
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
