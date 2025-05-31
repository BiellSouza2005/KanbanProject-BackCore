using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanProject.Entities
{
    public class TaskItem : Entity<int>
    {
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public bool ToDo { get; set; } = false;
        public bool Doing { get; set; } = false;
        public bool Done { get; set; } = false;
        public bool Testing { get; set; } = false;
        public bool Completed { get; set; } = false;

        public int? UserId { get; set; } // colaborador
        [ForeignKey("UserId")]
        public User User { get; set; }
        public DateTime DateTimeInclusion { get; set; }
        public string UserInclusion { get; set; } = string.Empty;
        public DateTime DateTimeChange { get; set; }
        public string UserChange { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

    }
}
