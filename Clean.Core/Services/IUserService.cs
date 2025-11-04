using Clean.Core.DTOs;
using Clean.Core.Entities;

namespace Clean.Core.Services
{
    /// <summary>
    /// Service interface for user management operations
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets all users in the system
        /// </summary>
        /// <returns>List of user response DTOs</returns>
        List<UserResponseDTO> GetUsers();
        
        /// <summary>
        /// Gets a user by their ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User response DTO or null if not found</returns>
        UserResponseDTO? GetById(int id);
        
        /// <summary>
        /// Gets a user by their username for display purposes
        /// </summary>
        /// <param name="username">Username to search for</param>
        /// <returns>User response DTO or null if not found</returns>
        UserResponseDTO? GetByUsername(string username);
        
        /// <summary>
        /// Gets a user entity by username for authentication purposes
        /// </summary>
        /// <param name="username">Username to search for</param>
        /// <returns>User entity with password or null if not found</returns>
        User? GetByUsernameForAuth(string username);
        
        /// <summary>
        /// Gets user ID by username
        /// </summary>
        /// <param name="username">Username to search for</param>
        /// <returns>User ID or null if not found</returns>
        int? GetUserIdByUsername(string username);
        
        /// <summary>
        /// Creates a new user account
        /// </summary>
        /// <param name="registerUserDto">Registration data</param>
        /// <returns>Created user response DTO</returns>
        UserResponseDTO Add(RegisterUserDTO registerUserDto);
        
        /// <summary>
        /// Updates an existing user
        /// </summary>
        /// <param name="userDto">Updated user data</param>
        /// <returns>Updated user response DTO</returns>
        UserResponseDTO Update(UserResponseDTO userDto);
        
        /// <summary>
        /// Deletes a user by ID
        /// </summary>
        /// <param name="id">User ID to delete</param>
        void Delete(int id);
    }
}