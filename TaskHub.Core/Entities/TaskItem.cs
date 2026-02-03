namespace TaskHub.Core.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }   
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Enums.State State { get; set; }


        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DeadLine { get; set; }
        public Enums.Priority Priority { get; set; }
        public User? User { get; set; }
    }
}
