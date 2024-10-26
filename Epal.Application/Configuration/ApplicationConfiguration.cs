using System.Reflection;
using Epal.Application.Features.EMailConfirmation.Services;
using Epal.Application.Interfaces;
using Epal.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;


namespace Epal.Application.Configuration;

public static class ApplicationConfiguration
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.AddMediatR(b => b.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IVerificationService, VerificationService>();
        services.AddMemoryCache();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();

        return services;
    }
}