using MediatR;

namespace TaskHub.Application.Services.TaskService.Command.CompleteTask
{
    public record CompleteTaskCommand(Guid TaskId, Guid UserId) : IRequest<bool>
    {
        private readonly Guid id;
        private readonly string? userIdString;
            
        public CompleteTaskCommand(Guid id, string userIdString)
            : this(id, Guid.TryParse(userIdString, out var guid) ? guid : Guid.Empty)
        {
            this.id = id;
            this.userIdString = userIdString;
        }
    }
}
