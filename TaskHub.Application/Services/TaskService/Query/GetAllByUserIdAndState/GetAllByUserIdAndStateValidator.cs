
using FluentValidation;
using TaskHub.Application.Services.TaskService.Query.GetAllByUserIdAndState;

namespace TaskHub.Application.Services.TaskService.Query.GetAllCompletedTask
{
    public class GetAllByUserIdAndStateValidator : AbstractValidator<GetAllByUserIdAndStateQuery>
    {
        public GetAllByUserIdAndStateValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.State)
                .IsInEnum()
                .WithMessage("State must be a valid enum value.");
        }
    }
}
