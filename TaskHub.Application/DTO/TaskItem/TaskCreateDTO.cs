using TaskHub.Core.Enums;

namespace TaskHub.Application.DTO.TaskItem
{
    public class TaskCreateDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } 
        public DateTime? DeadLine { get; set; }
        public float? HowMuchTime { get; set; }
        public Priority? Priority { get; set; }
    }
}
