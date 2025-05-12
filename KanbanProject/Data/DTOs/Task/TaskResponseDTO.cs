namespace KanbanProject.Data.DTOs.Task
{
    public class TaskResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Allocated { get; set; }
        public TaskStatus Status { get; set; }
        public string CreatedBy { get; set; }
        public string AssignedTo { get; set; }
    }
}
