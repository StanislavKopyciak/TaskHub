
using FluentValidation;

namespace TaskHub.Application.Services.TaskService.Command.GetAllCompletedTask
{
    public class GetAllCompletedTaskValidator : AbstractValidator<GetAllCompletedTaskCommand>
    {
        public GetAllCompletedTaskValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.State)
                .Equal(Core.Enums.State.Completed);
        }
    }
}
