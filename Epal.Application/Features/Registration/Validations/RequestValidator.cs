using FluentValidation;

namespace Epal.Application.Features.Registration.Validations;

public class RequestValidator : AbstractValidator<RegistrationRequest>
{

    public RequestValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email не может быть пустым")
            .EmailAddress().WithMessage("Неправильный формат email");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Пароль не может быть пустым")
            .MinimumLength(8).WithMessage("Пароль должен быть не менее 8 символов")
            .Must(password => password.Any(char.IsDigit)).WithMessage("Пароль должен содержать хотя бы одну цифру")
            .Must(password => password.Any(char.IsUpper)).WithMessage("Пароль должен содержать хотя бы одну заглавную букву");
        
    }
    
}