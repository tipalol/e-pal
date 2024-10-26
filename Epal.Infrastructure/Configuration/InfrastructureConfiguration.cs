using Epal.Application.Interfaces;
using Epal.Infrastructure.Database;
using Epal.Infrastructure.EmailServices;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Epal.Infrastructure.Configuration;

public static class InfrastructureConfiguration
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IEpalDbContext, EpalDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")!));
        services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }
}