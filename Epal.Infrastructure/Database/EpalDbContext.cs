using System.Reflection;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Epal.Infrastructure.Database;

public class EpalDbContext(DbContextOptions<EpalDbContext> options) : DbContext(options), IEpalDbContext
{
    public DbSet<Profile> Users { get; private  set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}