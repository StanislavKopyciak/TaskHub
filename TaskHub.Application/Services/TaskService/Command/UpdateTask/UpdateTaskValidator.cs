using FluentValidation;
using TaskHub.Application.Services.TaskService.Command.UpdateTask;

public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Поле 'Назва' є обов’язковим.")
            .MaximumLength(100).WithMessage("Максимальна довжина назви — 100 символів.");
    }
}
