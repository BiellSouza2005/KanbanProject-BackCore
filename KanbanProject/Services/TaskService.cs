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
                ToDo = true,
                Doing = false,
                Done = false,
                Testing = false,
                Completed = false,
                DateTimeInclusion = DateTime.UtcNow,
                UserInclusion = userInclusion,
                DateTimeChange = DateTime.UtcNow,
                UserChange = userInclusion,
                IsActive = true,
                DueDate = dto.DueDate
            };

            await _taskRepository.AddTaskAsync(newTask);
            return ConvertToDTO(newTask);
        }

        public async Task<TaskResponseDTO?> GetTaskByIdAsync(int id)
        {
            var taskEntity = await _taskRepository.GetTaskByIdAsync(id);
            return taskEntity == null || !taskEntity.IsActive ? null : ConvertToDTO(taskEntity);
        }

        public async Task<IEnumerable<TaskResponseDTO>> GetTasksByUserIdAsync(int? userId)
        {
            var taskEntities = await _taskRepository.GetTasksByUserIdAsync(userId);
            return taskEntities
                .Where(t => t.IsActive)
                .Select(ConvertToDTO);
        }


        public async Task<TaskResponseDTO?> UpdateTaskStatusAsync(int taskId, TaskUpdateDTO taskUpdateDTO, string userInclusion)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task == null || !task.IsActive)
                return null;

            task.Description = taskUpdateDTO.Description;
            task.UserId = taskUpdateDTO.UserId;
            task.ToDo = taskUpdateDTO.ToDo;
            task.Doing = taskUpdateDTO.Doing;
            task.Done = taskUpdateDTO.Done;
            task.Testing = taskUpdateDTO.Testing;
            task.Completed = taskUpdateDTO.Completed;
            task.DueDate = taskUpdateDTO.DueDate;
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
                UserId = taskEntity.UserId,
                DueDate = taskEntity.DueDate
            };
        }

        private bool IsUserAdmin(string userRequesting)
        {
            return userRequesting.ToLower().Contains("admin");
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null)
                return false;

            await _taskRepository.DeleteTaskAsync(task);
            return true;
        }

        public async Task<bool> DeactivateTaskAsync(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null || !task.IsActive)
                return false;

            task.IsActive = false;
            task.DateTimeChange = DateTime.UtcNow;
            task.UserChange = "system"; // ou o usu�rio que desativou

            await _taskRepository.UpdateTaskAsync(task);
            return true;
        }

    }
}
