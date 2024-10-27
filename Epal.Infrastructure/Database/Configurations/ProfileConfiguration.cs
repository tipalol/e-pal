using Epal.Domain.Entities;
using Epal.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Epal.Infrastructure.Database.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.Property(x => x.Status)
            .HasConversion(status => status.ToString(), s => Enum.Parse<UserStatus>(s));
        
        builder.HasIndex(x => x.Username);
    }
}