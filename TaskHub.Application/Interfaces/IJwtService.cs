namespace TaskHub.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId);
    }
}
