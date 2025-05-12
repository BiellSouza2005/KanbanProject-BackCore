using KanbanProject.Data;
using KanbanProject.Entities;
using KanbanProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using TaskEntity = KanbanProject.Entities.Task;

namespace KanbanProject.Repositories.Repos
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllTasksAsync()
        {
            return await _context.Tasks
                .Include(t => t.CreatedBy)
                .Include(t => t.AssignedTo)
                .ToListAsync();
        }

        //public async Task<TaskEntity> GetTaskByIdAsync(int id)
        //{
        //    return await _context.Tasks
        //        .Include(t => t.CreatedBy)
        //        .Include(t => t.AssignedTo)
        //        .FirstOrDefaultAsync(t => t.Id == id);
        //}

        public async Task AddTaskAsync(TaskEntity task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskEntity task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(TaskEntity task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}