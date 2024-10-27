using Epal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Interfaces;

public interface IEpalDbContext
{
    public DbSet<Profile> Users { get; }
    
    public Task<int> SaveChangesAsync(CancellationToken token);
}