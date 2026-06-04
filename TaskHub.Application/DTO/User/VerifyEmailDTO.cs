namespace TaskHub.Application.DTO.User
{
    public class VerifyEmailDTO
    {
        public string Email { get; set; } = null!;
        public string Code { get; set; } = null!;
    }
}
