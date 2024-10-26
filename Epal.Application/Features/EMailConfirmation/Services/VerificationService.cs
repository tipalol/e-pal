﻿using Epal.Application.Features.Registration;
using Epal.Application.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Epal.Application.Features.EMailConfirmation.Services;

public class VerificationService(IMemoryCache cache, IEmailSender emailSender) : IVerificationService
{
    public async Task SendVerificationCode(string email)
    {
        int verificationCode = CreateVerificationCode();
        SaveCodeInCache(email, verificationCode);
        string body =
            $"<div style=\"color: black;\">Сообщение от NPL. Ваш код подтверждения <div style=\"color: red;\"> <br>{verificationCode}</br> </div></div>";
        await emailSender.SendEmailAsync(email, "Confirmation Email", body);
    }

    public bool Verify(string email, int verificationCode)
    {
        if (cache.TryGetValue(email, out int cacheVerificationCode))
        {
            if (cacheVerificationCode == verificationCode)
                return true;
        }
        return false;
    }

    private int CreateVerificationCode() => Math.Abs(Guid.NewGuid().GetHashCode() % 100000);

    private void SaveCodeInCache(string email, int code) =>
        cache.Set(email, code, new DateTimeOffset(DateTime.UtcNow.AddHours(4)));
}