using Epal.Domain.Entities;
using Epal.Domain.Entities.Base;
using Epal.Domain.Entities.ManyToMany;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Interfaces;

public interface IEpalDbContext
{
    public DbSet<Profile> Users { get; }
    public DbSet<Service> Services { get; }
    public DbSet<ProfileServices> ProfileServices { get; }

    public Task<int> SaveChangesAsync(CancellationToken token);
}
