using Epal.Domain.Entities;
using Epal.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Epal.Infrastructure.Database.Configurations;

public class ActivityInProfileConfiguration: IEntityTypeConfiguration<ActivityInProfile>
{
    public void Configure(EntityTypeBuilder<ActivityInProfile> builder)
    {
        builder.HasKey(e => new { e.ActivityId, e.ProfileId});
        
        builder
            .HasOne(e => e.Profile)
            .WithMany()
            .HasForeignKey(e => e.ProfileId) 
            .OnDelete(DeleteBehavior.Restrict); 

        builder
            .HasOne(e => e.Activity)
            .WithMany() 
            .HasForeignKey(e => e.ActivityId) 
            .OnDelete(DeleteBehavior.Restrict); 
    }


}