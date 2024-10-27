﻿using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Registration;

public record RegistrationRequest(string Email, string Password) : IRequest;

internal sealed class Handler(IEpalDbContext context, IPasswordService passwordService, IVerificationService verificationService) : IRequestHandler<RegistrationRequest>
{
    public async Task Handle(RegistrationRequest request, CancellationToken cancellationToken)
    {
        var userExists = await context.Users
            .AnyAsync(x => x.Email == request.Email, cancellationToken);
        
        if (userExists)
        {
            throw new ArgumentException("Пользователь с таким Email уже существует");
        }

        verificationService.SendVerificationCodeAsync(request.Email);  
        
        var passwordHash = passwordService.HashPassword(request.Password);
        var user = new Profile(request.Email, passwordHash);

        var username = Math.Abs(user.Id.GetHashCode() % 10000000).ToString();
        if (await context.Users.AnyAsync(x => x.Username == username, cancellationToken))
        {
            username = Math.Abs(user.Id.GetHashCode() % 10123321).ToString();
        }
        user.Username = username;
        
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}