using Epal.Application.Features.Registration.Post;
using FluentValidation;

namespace Epal.Application.Features.Registration.Validations;

public class DtoValidator : AbstractValidator<CreateRegisterDtoRequest>
{

    public DtoValidator()
    {
        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Имя пользователя не может быть пустым")
            .MinimumLength(3).WithMessage("Имя пользователя должно быть не менее 3 символов")
            .MaximumLength(50).WithMessage("Имя пользователя не может быть длиннее 50 символов");

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