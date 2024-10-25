using Epal.Application.Features.Users.Add;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Registration.Post;

public record CreateRegisterDtoRequest(string Username, string Email, string Password) : IRequest;

internal sealed class Handler(IEpalDbContext context, IPasswordService passwordService) : IRequestHandler<CreateRegisterDtoRequest>
{
    public async Task Handle(CreateRegisterDtoRequest request, CancellationToken cancellationToken)
    {
        var exsistUser = await context.Users.FirstOrDefaultAsync(x => x.Username == request.Username || x.Email == request.Email, cancellationToken:cancellationToken);
        if (exsistUser != null)
        {
            if (request.Email == exsistUser.Email)
                throw new ArgumentException("Пользователь с таким Email уже существует");
            throw new AggregateException("Пользователь с таким именем пользователя уже существует");
        }
        var passwordHash = passwordService.HashPassword(request.Password);
        var user = new User(request.Username, request.Email, passwordHash);
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}