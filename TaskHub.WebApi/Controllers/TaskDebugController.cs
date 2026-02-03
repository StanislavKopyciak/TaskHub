using Microsoft.AspNetCore.Mvc;
using TaskHub.Application.Services.TaskService; // твій TaskService інтерфейс

namespace TaskHub.WebApi.Controllers
{
    [ApiController]
    [Route("api/debug/tasks")]
    public class TaskDebugController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskDebugController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // Перевірити CreatedAt по юзеру
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTasks(Guid userId)
        {
            var tasks = await _taskService.GetAllByUserIdAsync(userId);

            return Ok(tasks.Select(t => new
            {
                t.Id,
                t.Title,
                t.CreatedAt,
                t.UpdatedAt,
                t.DeadLine
            }));
        }
    }
}
