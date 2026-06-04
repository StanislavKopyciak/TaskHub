namespace TaskHub.Core.Entities
{
    public class EmailVerification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime Expiration { get; set; } = DateTime.UtcNow.AddMinutes(15);
        
        public bool IsUsed { get; set; } = false;

        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
