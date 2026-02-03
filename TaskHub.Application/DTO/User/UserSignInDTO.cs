using System.ComponentModel.DataAnnotations;

namespace TaskHub.Application.DTO.User
{
    public class UserSignInDTO
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
