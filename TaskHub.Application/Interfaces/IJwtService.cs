namespace TaskHub.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(Guid userId);
        bool ValidateAccessToken(string token);
    }
}
