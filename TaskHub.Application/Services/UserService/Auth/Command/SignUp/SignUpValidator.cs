using FluentValidation;
namespace TaskHub.Application.Services.UserService.Auth.Command.SignUp
{
    public class SignUpValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("І'мя повинно бути заповненим.")
                .MaximumLength(50).WithMessage("І'мя не може перевищувати 50 символів.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email повинен бути заповненим.")
                .EmailAddress().WithMessage("Невірний формат email.")
                .MaximumLength(255).WithMessage("Максимальна довжина Email 255 символів.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль повинен бути заповненим.")
                .MinimumLength(6).WithMessage("Мінімальна довжина паролю 6 символів.")
                .MaximumLength(100).WithMessage("Максимальна довжина паролю 100 символів");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Пароль для пітвердження повинен бути заповненим.")
                .Equal(x => x.Password).WithMessage("Паролі не співпадають.");
        }
    }
}
