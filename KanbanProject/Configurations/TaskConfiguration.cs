using KanbanProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanProject.Configurations
{
    public class TaskConfigurations : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.ToDo).IsRequired();
            builder.Property(t => t.Doing).IsRequired();
            builder.Property(t => t.Done).IsRequired();
            builder.Property(t => t.Testing).IsRequired();
            builder.Property(t => t.Completed).IsRequired();

            builder.Property(t => t.DueDate)
                .IsRequired(false);

            builder.Property(t => t.DateTimeInclusion).IsRequired();            
            builder.Property(t => t.UserInclusion)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.DateTimeChange).IsRequired();
            builder.Property(t => t.UserChange)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(t => t.IsActive).IsRequired();

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
