using System.Reflection;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using Epal.Domain.Entities.Base;
using Epal.Domain.Entities.ManyToMany;
using Microsoft.EntityFrameworkCore;

namespace Epal.Infrastructure.Database;

public class EpalDbContext(DbContextOptions<EpalDbContext> options) : DbContext(options), IEpalDbContext
{
    public DbSet<Profile> Users { get; private  set; }
    public DbSet<Service> Services { get; private  set;}

    public DbSet<ProfileServices> ProfileServices { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<DateTrackedEntity>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.Updated = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
