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
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] TaskUpdateStatusDTO updateDto)
        {
            try
            {
                var updatedTask = await _taskService.UpdateTaskStatusAsync(id, updateDto.NewStatus, updateDto.UserRequesting);
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

    }
}
