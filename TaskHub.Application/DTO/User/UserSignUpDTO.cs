using System.ComponentModel.DataAnnotations;

namespace TaskHub.Application.DTO.User
{
    public class UserSignUpDTO
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string ConfirmPassword { get; set; } = null!;
    }
}
