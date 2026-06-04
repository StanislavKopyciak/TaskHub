namespace TaskHub.Application.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(string to, string subject, string body, CancellationToken ct);
    }
}
