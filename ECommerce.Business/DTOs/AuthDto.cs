using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        [RegularExpression(@"^(?=.*\d).+$", ErrorMessage = "Password must contain at least one number.")]
        public string Password { get; set; }

        public int Role { get; set; }
    }
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
        
    public class RegisterResponseDto
    {
        public long UserId { get; set; }
        public int Role { get; set; }
        public string Email { get; set; }
    }
    public class AuthResponseDto : RegisterResponseDto
    {
        public string Token { get; set; }
      
    }

}
