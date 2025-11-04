using Clean.Core.DTOs;
using Clean.Core.Services;
using Clean.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Clean.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;

        public TaskController(ITaskService taskService, IUserService userService)
        {
            _taskService = taskService;
            _userService = userService;
        }

        /// <summary>
        /// Gets tasks for the authenticated user only
        /// Security: Users can only see their own tasks
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetMyTasks()
        {
            // Extract user identifier from JWT token
            var userIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdentifier))
            {
                return Unauthorized(new { Message = "No user identifier claim found" });
            }

            // Try to parse as user ID first, if that fails try as username
            int? userId = null;
            if (int.TryParse(userIdentifier, out int parsedId))
            {
                userId = parsedId;
            }
            else
            {
                userId = _userService.GetUserIdByUsername(userIdentifier);
            }

            if (!userId.HasValue)
            {
                return Unauthorized(new { Message = "User not found", UserIdentifier = userIdentifier });
            }

            // Return only tasks belonging to this user
            var tasks = await _taskService.GetTasksByUserIdAsync(userId.Value);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDTO>> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        /// <summary>
        /// Creates a new task for the authenticated user
        /// Requires valid JWT token with user ID claim
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TaskDTO>> CreateTask(TaskInputDTO taskInputDto)
        {
            // Validate model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Extract user identifier from JWT token
            var userIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdentifier))
            {
                return Unauthorized(new { Message = "No user identifier claim found" });
            }

            // Try to parse as user ID first, if that fails try as username
            int? userId = null;
            if (int.TryParse(userIdentifier, out int parsedId))
            {
                userId = parsedId;
            }
            else
            {
                userId = _userService.GetUserIdByUsername(userIdentifier);
            }

            if (!userId.HasValue)
            {
                return Unauthorized(new { Message = "User not found", UserIdentifier = userIdentifier });
            }

            try
            {
                // Create task with user ID from token
                var createdTask = await _taskService.CreateTaskAsync(taskInputDto, userId.Value);
                
                // Return 201 Created with location header
                return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDTO>> UpdateTask(int id, TaskInputDTO taskInputDto)
        {
            // Validate model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            try
            {
                var updatedTask = await _taskService.UpdateTaskAsync(id, taskInputDto, userId);
                return Ok(updatedTask);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            try
            {
                await _taskService.DeleteTaskAsync(id, userId);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}