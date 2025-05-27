using KanbanProject.Data.DTOs.Task;
using KanbanProject.Entities;
using KanbanProject.Repositories.Interfaces;

namespace KanbanProject.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskResponseDTO> CreateTaskAsync(TaskCreateDTO dto, string userInclusion)
        {
            var newTask = new TaskItem
            {
                Description = dto.Description,
                UserId = dto.UserId,
                ToDo = true,
                Doing = false,
                Done = false,
                Testing = false,
                Completed = false,
                DateTimeInclusion = DateTime.UtcNow,
                UserInclusion = userInclusion,
                DateTimeChange = DateTime.UtcNow,
                UserChange = userInclusion,
                IsActive = true
            };

            await _taskRepository.AddTaskAsync(newTask);
            return ConvertToDTO(newTask);
        }

        public async Task<TaskResponseDTO?> GetTaskByIdAsync(int id)
        {
            var taskEntity = await _taskRepository.GetTaskByIdAsync(id);
            return taskEntity == null ? null : ConvertToDTO(taskEntity);
        }

        public async Task<TaskResponseDTO?> UpdateTaskStatusAsync(int taskId, TaskUpdateDTO taskUpdateDTO, string userInclusion)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task == null || !task.IsActive)
                return null;

            task.Description = taskUpdateDTO.Description;
            task.ToDo = taskUpdateDTO.ToDo;
            task.Doing = taskUpdateDTO.Doing;
            task.Done = taskUpdateDTO.Done;
            task.Testing = taskUpdateDTO.Testing;
            task.Completed = taskUpdateDTO.Completed;
            task.DateTimeChange = DateTime.UtcNow;
            task.UserChange = userInclusion;

            await _taskRepository.UpdateTaskAsync(task);
            return ConvertToDTO(task);
        }

        private TaskResponseDTO ConvertToDTO(TaskItem taskEntity)
        {
            return new TaskResponseDTO
            {
                TaskId = taskEntity.Id,
                Description = taskEntity.Description,
                ToDo = taskEntity.ToDo,
                Doing = taskEntity.Doing,
                Done = taskEntity.Done,
                Testing = taskEntity.Testing,
                Completed = taskEntity.Completed,
                UserId = taskEntity.UserId
            };
        }

        private bool IsUserAdmin(string userRequesting)
        {
            return userRequesting.ToLower().Contains("admin");
        }
    }
}
