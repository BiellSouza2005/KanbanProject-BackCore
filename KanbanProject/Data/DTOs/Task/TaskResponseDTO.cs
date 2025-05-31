namespace KanbanProject.Data.DTOs.Task
{
    public class TaskResponseDTO
    {
        public int TaskId { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool ToDo { get; set; }
        public bool Doing { get; set; }
        public bool Done { get; set; }
        public bool Testing { get; set; }
        public bool Completed { get; set; }
        public int? UserId { get; set; }

    }
}
