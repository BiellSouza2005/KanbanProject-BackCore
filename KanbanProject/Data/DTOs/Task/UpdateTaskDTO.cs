namespace KanbanProject.Data.DTOs.Task
{
    public class UpdateTaskDTO
    {
        public string Name { get; set; }
        public int? AssignedToId { get; set; }
        public TaskStatus? Status { get; set; }
    }
}
