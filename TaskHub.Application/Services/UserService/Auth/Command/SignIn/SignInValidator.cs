using FluentValidation;

namespace TaskHub.Application.Services.UserService.Auth.Command.SignIn
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        public SignInValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email повинен бути заповнений.")
                .EmailAddress().WithMessage("Невірний email.")
                .MaximumLength(255).WithMessage("максимальний розмір Email 255 символів.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль повинен бути заповнений.")
                .MinimumLength(6).WithMessage("Мінімальна довжина паролю 6 символів")
                .MaximumLength(100).WithMessage("Максимальна довжина паролю 100 символів.");
        }
    }
}
