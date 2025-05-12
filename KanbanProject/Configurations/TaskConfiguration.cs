using KanbanProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KanbanProject.Configurations
{
    public class TaskConfigurations : IEntityTypeConfiguration<Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Entities.Task> builder)
        {
            // Mapeia para a tabela "Tasks"
            builder.ToTable("Tasks");

            // Chave primária e auto-incremento
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                   .ValueGeneratedOnAdd();

            // Name: obrigatório e com tamanho máximo
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            // Allocated: obrigatório e default = false
            builder.Property(t => t.Allocated)
                   .IsRequired()
                   .HasDefaultValue(false);

            // Status: obrigatório e default = ToDo
            builder.Property(t => t.Status)
                   .IsRequired()
                   .HasDefaultValue(Entities.TaskStatus.ToDo);

            // CreatedById: obrigatório
            builder.Property(t => t.CreatedById)
                   .IsRequired();

            // FK para quem criou (CreatedById)
            builder.HasOne(t => t.CreatedBy)
                   .WithMany()  
                   .HasForeignKey(t => t.CreatedById)
                   .OnDelete(DeleteBehavior.Restrict);

            // FK para quem foi atribuído (AssignedToId)
            builder.HasOne(t => t.AssignedTo)
                   .WithMany()  
                   .HasForeignKey(t => t.AssignedToId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}