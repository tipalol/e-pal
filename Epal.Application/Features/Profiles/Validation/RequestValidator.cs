using Epal.Application.Features.Profiles.Models;
using FluentValidation;

namespace Epal.Application.Features.Profiles.Validation;

public class RequestValidator : AbstractValidator<ProfileModel>
{

    public RequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
                .WithMessage("Имя пользователя не может быть пустым")
            .MinimumLength(4)
                .WithMessage("Слишком короткое имя пользователя")
            .MaximumLength(30)
                .WithMessage("Слишком длинное имя пользователя")
            .Matches(@"^[a-zA-Z0-9_.-]+$")
                .WithMessage("Имя пользователя может содержать только буквы, цифры, дефисы, точки и нижнее подчеркивание.");
    }
    
}