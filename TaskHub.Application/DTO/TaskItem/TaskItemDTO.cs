using System;
using TaskHub.Core.Entities;
using TaskHub.Core.Enums;

namespace TaskHub.Application.DTO.TaskItem
{
    public class TaskItemDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } 
        public DateTime? DeadLine { get; set; }
        public State State { get; set; }
        public float? HowMuchTime { get; set; }
        public Priority? Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
