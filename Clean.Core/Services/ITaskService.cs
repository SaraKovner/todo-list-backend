using Clean.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clean.Core.Services
{
    /// <summary>
    /// Service interface for task management operations
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Gets a specific task by ID
        /// </summary>
        /// <param name="id">Task ID</param>
        /// <returns>Task DTO or null if not found</returns>
        Task<TaskDTO?> GetTaskByIdAsync(int id);
        
        /// <summary>
        /// Gets all tasks belonging to a specific user
        /// </summary>
        /// <param name="userId">User ID to get tasks for</param>
        /// <returns>Collection of task DTOs</returns>
        Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId);
        
        /// <summary>
        /// Creates a new task for the specified user
        /// </summary>
        /// <param name="taskInputDto">Task data to create</param>
        /// <param name="userId">User ID who owns the task</param>
        /// <returns>Created task DTO</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when category doesn't belong to user</exception>
        Task<TaskDTO> CreateTaskAsync(TaskInputDTO taskInputDto, int userId);
        
        /// <summary>
        /// Updates an existing task, ensuring it belongs to the user
        /// </summary>
        /// <param name="id">Task ID to update</param>
        /// <param name="taskInputDto">Updated task data</param>
        /// <param name="userId">User ID for authorization</param>
        /// <returns>Updated task DTO</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when task or category doesn't belong to user</exception>
        Task<TaskDTO> UpdateTaskAsync(int id, TaskInputDTO taskInputDto, int userId);
        
        /// <summary>
        /// Deletes a task, ensuring it belongs to the user
        /// </summary>
        /// <param name="id">Task ID to delete</param>
        /// <param name="userId">User ID for authorization</param>
        /// <exception cref="UnauthorizedAccessException">Thrown when task doesn't belong to user</exception>
        Task DeleteTaskAsync(int id, int userId);
    }
}