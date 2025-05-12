using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanProject.Entities
{
    public class Task : Entity<int>
    {
        [Required]
        public string Name { get; set; }

        public bool Allocated { get; set; } = false;

        public TaskStatus Status { get; set; } = TaskStatus.ToDo;

        [Required]
        public int CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }

        public int? AssignedToId { get; set; }

        [ForeignKey("AssignedToId")]
        public User AssignedTo { get; set; }
    }

    public enum TaskStatus
    {
        ToDo = 0,
        InProgress = 1,
        Done = 2
    }
}