using FluentValidation;

namespace Epal.Application.Features.Authorize.Validation;

public class RequestValidator : AbstractValidator<AuthorizeRequest>
{
    public RequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Имя пользователя не может быть пустым")
            .MinimumLength(4)
            .WithMessage("Слишком короткое имя пользователя")
            .MaximumLength(30)
            .WithMessage("Слишком длинное имя пользователя");
    }
}