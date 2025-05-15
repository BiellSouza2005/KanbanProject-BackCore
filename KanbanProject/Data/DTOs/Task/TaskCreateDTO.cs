using KanbanProject.Data.DTOs.User;
namespace KanbanProject.Data.DTOs.Task

{
    public class TaskCreateDTO
    {
        public string Description { get; set; }
        public int UserId { get; set; }
        public int? AdminId { get; set; }
    }
}
