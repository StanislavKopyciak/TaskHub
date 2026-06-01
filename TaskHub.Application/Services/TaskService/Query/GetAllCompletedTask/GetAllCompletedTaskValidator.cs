
using FluentValidation;

namespace TaskHub.Application.Services.TaskService.Query.GetAllCompletedTask
{
    public class GetAllCompletedTaskValidator : AbstractValidator<GetAllCompletedTaskQuery>
    {
        public GetAllCompletedTaskValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.State)
                .Equal(Core.Enums.State.Completed);
        }
    }
}
