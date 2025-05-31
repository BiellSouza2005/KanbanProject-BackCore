using KanbanProject.Data.DTOs;
using KanbanProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using KanbanProject.Services;
using KanbanProject.Data.DTOs.Task;


namespace KanbanProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDTO dto, [FromHeader(Name = "User-Inclusion")] string userInclusion)
        {
            var taskDto = await _taskService.CreateTaskAsync(dto, userInclusion);

            return CreatedAtAction(nameof(GetTaskById), new { id = taskDto.TaskId }, taskDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var taskDto = await _taskService.GetTaskByIdAsync(id);

            if (taskDto == null)
                return NotFound();

            return Ok(taskDto);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetTasksByUserId([FromQuery] int? userId)
        {
            var taskDtos = await _taskService.GetTasksByUserIdAsync(userId);

            if (!taskDtos.Any())
                return NotFound();

            return Ok(taskDtos);
        }

        [HttpPut("/api/Tasks/status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] TaskUpdateDTO taskUpdateDto, [FromHeader(Name = "User-Inclusion")] string userInclusion)
        {
            try
            {
                var updatedTask = await _taskService.UpdateTaskStatusAsync(id, taskUpdateDto, userInclusion);
                if (updatedTask == null)
                    return NotFound();

                return Ok(updatedTask);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound("Task não encontrada.");

            var deleted = await _taskService.DeleteTaskAsync(id);
            if (!deleted)
                return StatusCode(500, "Erro ao deletar a task.");

            return NoContent(); // 204
        }

        [HttpPatch("/api/Tasks/deactivate/{id}")]
        public async Task<IActionResult> DeactivateTask(int id)
        {
            var updated = await _taskService.DeactivateTaskAsync(id);
            if (!updated)
                return NotFound("Task não encontrada ou já está desativada.");

            return Ok("Task desativada com sucesso.");
        }


    }
}
