﻿using KanbanProject.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KanbanProject.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.IsAdmin)
                   .HasDefaultValue(false);

            builder.Property(c => c.DateTimeInclusion)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UserInclusion)
                   .IsRequired()
                   .HasMaxLength(80);

            builder.Property(c => c.DateTimeChange)
                   .IsRequired();

            builder.Property(c => c.UserChange)
                   .HasMaxLength(80);

            builder.Property(c => c.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);

        }
    }
}
