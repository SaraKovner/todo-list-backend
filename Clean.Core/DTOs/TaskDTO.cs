namespace Clean.Core.DTOs
{
    /// <summary>
    /// Data Transfer Object for Task information
    /// </summary>
    public class TaskDTO
    {
        /// <summary>
        /// Unique identifier for the task
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Task title/name
        /// </summary>
        public string Title { get; set; } = string.Empty;
        
        /// <summary>
        /// Detailed description of the task
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Indicates whether the task is completed
        /// </summary>
        public bool IsCompleted { get; set; }
        
        /// <summary>
        /// Task priority level (Low, Medium, High)
        /// </summary>
        public string Priority { get; set; } = "Low";
        
        /// <summary>
        /// Category information for the task
        /// </summary>
        public CategoryDTO Category { get; set; } = new();
    }
}