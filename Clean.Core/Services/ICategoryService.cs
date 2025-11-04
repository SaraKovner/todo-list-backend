using Clean.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clean.Core.Services
{
    /// <summary>
    /// Service interface for category management operations
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Gets all categories belonging to a specific user
        /// </summary>
        /// <param name="userId">User ID to get categories for</param>
        /// <returns>Collection of category DTOs</returns>
        Task<IEnumerable<CategoryDTO>> GetCategoriesByUserIdAsync(int userId);
        
        /// <summary>
        /// Gets a specific category by ID, ensuring it belongs to the user
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <param name="userId">User ID for authorization</param>
        /// <returns>Category DTO or null if not found or unauthorized</returns>
        Task<CategoryDTO?> GetCategoryByIdAsync(int id, int userId);
        
        /// <summary>
        /// Creates a new category for the specified user
        /// </summary>
        /// <param name="categoryDto">Category data to create</param>
        /// <param name="userId">User ID who owns the category</param>
        /// <returns>Created category DTO</returns>
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto, int userId);
        
        /// <summary>
        /// Updates an existing category, ensuring it belongs to the user
        /// </summary>
        /// <param name="id">Category ID to update</param>
        /// <param name="categoryDto">Updated category data</param>
        /// <param name="userId">User ID for authorization</param>
        /// <returns>Updated category DTO</returns>
        Task<CategoryDTO> UpdateCategoryAsync(int id, CategoryDTO categoryDto, int userId);
        
        /// <summary>
        /// Deletes a category, ensuring it belongs to the user
        /// </summary>
        /// <param name="id">Category ID to delete</param>
        /// <param name="userId">User ID for authorization</param>
        Task DeleteCategoryAsync(int id, int userId);
    }
}