namespace KanbanProject.Data.DTOs.Task
{
    public class CreateTaskDTO
    {
        public required string Name { get; set; }
        public int? AssignedToId { get; set; } 
    }
}
