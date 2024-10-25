﻿using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Registration;

public record RegistrationRequest(string Email, string Password) : IRequest;

internal sealed class Handler(IEpalDbContext context, IPasswordService passwordService) : IRequestHandler<RegistrationRequest>
{
    public async Task Handle(RegistrationRequest request, CancellationToken cancellationToken)
    {
        var userExists = await context.Users
            .AnyAsync(x => x.Email == request.Email, cancellationToken);
        
        if (userExists)
        {
            throw new ArgumentException("Пользователь с таким Email уже существует");
        }
        
        var passwordHash = passwordService.HashPassword(request.Password);
        var user = new User(request.Email, passwordHash);
        
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}