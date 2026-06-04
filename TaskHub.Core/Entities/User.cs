namespace TaskHub.Core.Entities
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EmailVerified { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<TaskItem>? Tasks { get; set; }
    }
}
