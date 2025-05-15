namespace KanbanProject.Data.DTOs.Task
{
    public class UpdateTaskDTO
    {
        public int TaskId { get; set; }

        public bool ToDo { get; set; }
        public bool Doing { get; set; }
        public bool Done { get; set; }
        public bool Testing { get; set; }
        public bool Completed { get; set; }
    }
}
