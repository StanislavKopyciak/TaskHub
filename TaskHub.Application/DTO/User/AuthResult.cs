using TaskHub.Application.DTO.User;

public class AuthResult
{
    public string? Token { get; set; }
    public UserDTO? User { get; set; }
}