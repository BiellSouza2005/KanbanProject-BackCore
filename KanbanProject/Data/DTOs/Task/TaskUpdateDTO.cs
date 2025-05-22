namespace KanbanProject.Data.DTOs.Task
{
    public class TaskUpdateDTO
    {
        public int TaskId { get; set; }

        public string Description { get; set; }
        public bool ToDo { get; set; }
        public bool Doing { get; set; }
        public bool Done { get; set; }
        public bool Testing { get; set; }
        public bool Completed { get; set; }
    }
}
