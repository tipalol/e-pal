using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Epal.Infrastructure.Database.Factories;

public class EpalDbContextFactory : IDesignTimeDbContextFactory<EpalDbContext>
{
    private readonly IConfiguration _configuration;

    public EpalDbContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public EpalDbContextFactory()
    {
    }

    public EpalDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EpalDbContext>();
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")!);

        return new EpalDbContext(optionsBuilder.Options);
    }
}