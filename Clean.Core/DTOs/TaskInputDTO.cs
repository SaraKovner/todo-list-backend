using System.ComponentModel.DataAnnotations;

namespace Clean.Core.DTOs
{
    public class TaskInputDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 50 characters")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be a positive number")]
        public int CategoryId { get; set; }

        [StringLength(6, ErrorMessage = "Priority cannot exceed 6 characters")]
        public string Priority { get; set; } = "Low";
    }
}