namespace TaskHub.Core.Entities
{
    public class User
    {
        public Guid UserId { get; set; } 
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public ICollection<TaskItem>? Tasks { get; set; }
    }
}
