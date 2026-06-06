namespace TaskHub.Core.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(7);
        public Guid UserId { get; set; }

    }
}
