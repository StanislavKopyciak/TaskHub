namespace TaskHub.Application.DTO.User;
public class AuthResult
{
    public string? RefreshToken { get; set; }
    public string? AccessToken { get; set; }
}