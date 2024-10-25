using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Epal.Application.Configuration;

public static class ApplicationConfiguration
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.AddMediatR(b => b.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}