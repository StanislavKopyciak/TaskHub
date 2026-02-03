using FluentValidation;
using TaskHub.Application.Services.TaskService.Command.CreateTask;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Поле 'Назва' є обов’язковим.")
            .MaximumLength(100).WithMessage("Максимальна довжина назви — 100 символів.");
    }
}

