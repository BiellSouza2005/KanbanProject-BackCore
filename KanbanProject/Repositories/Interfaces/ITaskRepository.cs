using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanProject.Entities;
using Task = System.Threading.Tasks.Task;
using TaskEntity = KanbanProject.Entities.Task;

namespace KanbanProject.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntity>> GetAllTasksAsync();
        //Task<TaskEntity> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TaskEntity task);
        Task UpdateTaskAsync(TaskEntity task);
        Task DeleteTaskAsync(TaskEntity task);
    }
}