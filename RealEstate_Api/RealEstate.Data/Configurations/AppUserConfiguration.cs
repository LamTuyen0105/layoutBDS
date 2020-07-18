using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(x => x.Avatar).IsRequired(false);
            builder.Property(x => x.FullName).HasMaxLength(200);
            builder.Property(x => x.Gender).HasMaxLength(5);
            builder.Property(x => x.IdentityNumber).HasMaxLength(10);
            builder.HasOne(x => x.Ward).WithMany(x => x.AppUsers).HasForeignKey(x => x.WardId);
        }
    }
}
