using Epal.Application.Features.Registration.Models;
using Epal.Application.Features.Users.Add;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Epal.Application.Features.Registration.Post;

public record CreateRegisterDtoRequest(string Username, string Email, string Password, string PasswordConfirm) : IRequest;

internal sealed class Handler(IEpalDbContext context, AbstractValidator<RegisterDto> dtoValidator) : IRequestHandler<CreateRegisterDtoRequest>
{
    public async Task Handle(CreateRegisterDtoRequest request, CancellationToken cancellationToken)
    {
        var model = new RegisterDto(Username: request.Username, Email: request.Email, Password: request.Password,
            PasswordConfirm: request.PasswordConfirm);
        var validationResult = await dtoValidator.ValidateAsync(model, cancellationToken);
        if (validationResult.IsValid)
            Console.WriteLine("Valide register request");
        foreach(var v in validationResult.Errors)
            Console.WriteLine(v.ErrorMessage);
            
            
    }
}