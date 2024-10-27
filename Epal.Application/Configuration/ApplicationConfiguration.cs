using System.Reflection;
using System.Text.Json.Serialization;
using Epal.Application.Interfaces;
using Epal.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
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

        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IVerificationService, VerificationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddMemoryCache();
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        
        return services;
    }
}