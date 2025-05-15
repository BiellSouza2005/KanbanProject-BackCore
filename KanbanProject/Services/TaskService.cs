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
                AdminId = dto.AdminId,
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

        public async Task<TaskResponseDTO?> UpdateTaskStatusAsync(int taskId, string newStatus, string userRequesting)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task == null || !task.IsActive)
                return null;

            string currentStatus = GetCurrentStatus(task);

            if (!IsNextStatusValid(currentStatus, newStatus))
                throw new InvalidOperationException($"Não é permitido pular status de '{currentStatus}' para '{newStatus}'.");

            if (newStatus == "Completed" && !IsUserAdmin(userRequesting))
                throw new UnauthorizedAccessException("Apenas admin pode marcar como Completed.");

            ResetAllStatus(task);
            SetStatusBoolean(task, newStatus);

            task.DateTimeChange = DateTime.UtcNow;
            task.UserChange = userRequesting;

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
                AdminId = taskEntity.AdminId
            };
        }

        private string GetCurrentStatus(TaskItem task)
        {
            if (task.ToDo) return "ToDo";
            if (task.Doing) return "Doing";
            if (task.Done) return "Done";
            if (task.Testing) return "Testing";
            if (task.Completed) return "Completed";
            return "Unknown";
        }

        private bool IsNextStatusValid(string current, string next)
        {
            var allowedTransitions = new Dictionary<string, string>
            {
                { "ToDo", "Doing" },
                { "Doing", "Done" },
                { "Done", "Testing" },
                { "Testing", "Completed" }
            };
            return allowedTransitions.TryGetValue(current, out var expectedNext) && expectedNext == next;
        }

        private void ResetAllStatus(TaskItem task)
        {
            task.ToDo = false;
            task.Doing = false;
            task.Done = false;
            task.Testing = false;
            task.Completed = false;
        }

        private void SetStatusBoolean(TaskItem task, string status)
        {
            switch (status)
            {
                case "ToDo": task.ToDo = true; break;
                case "Doing": task.Doing = true; break;
                case "Done": task.Done = true; break;
                case "Testing": task.Testing = true; break;
                case "Completed": task.Completed = true; break;
                default: throw new InvalidOperationException("Status inválido.");
            }
        }

        private bool IsUserAdmin(string userRequesting)
        {
            return userRequesting.ToLower().Contains("admin");
        }
    }
}
