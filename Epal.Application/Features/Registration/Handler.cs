using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Epal.Application.Features.Registration;

public record RegistrationRequest(string Email, string Password) : IRequest;

internal sealed class Handler(IEpalDbContext context, IPasswordService passwordService, IEmailSender emailSender, IMemoryCache cache) : IRequestHandler<RegistrationRequest>
{
    public async Task Handle(RegistrationRequest request, CancellationToken cancellationToken)
    {
        var userExists = await context.Users
            .AnyAsync(x => x.Email == request.Email, cancellationToken);
        
        if (userExists)
        {
            throw new ArgumentException("Пользователь с таким Email уже существует");
        }

        // TODO внести в EMailConfirmationService
        var verificationCode = CreateVerificationCode(request);
        SaveCodeInCache(request, verificationCode);
        SendVerificationCode(request, verificationCode);
        Console.WriteLine(verificationCode);
        //var passwordHash = passwordService.HashPassword(request.Password);
        //var user = new User(request.Email, passwordHash);

        //await context.Users.AddAsync(user, cancellationToken);
        //await context.SaveChangesAsync(cancellationToken);
    }

    public int CreateVerificationCode(RegistrationRequest request) => Math.Abs(Guid.NewGuid().GetHashCode() % 100000);
    private void SaveCodeInCache(RegistrationRequest request, int code)=>
        cache.Set(request.Email, code, new DateTimeOffset(DateTime.UtcNow.AddHours(4)));
    private async Task SendVerificationCode(RegistrationRequest request, int code)
    {
        string body = $"<div style=\"color: black;\">Сообщение от NPL. Ваш код подтверждения <div style=\"color: red;\"> <br>{code}</br> </div></div>";
        await emailSender.SendEmailAsync(request.Email, "Confirmation Email", body);
    }
    
}