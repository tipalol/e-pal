using System.Reflection;
using Epal.Application.Features.Registration.Validations;
using Epal.Application.Interfaces;
using Epal.Application.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Epal.Application.Configuration;

public static class ApplicationConfiguration
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.AddMediatR(b => b.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IPasswordService, PasswordService>();

        return services;
    }
}