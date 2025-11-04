using System;

namespace Clean.Core.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Priority { get; set; } = "Low";

        // Foreign Keys
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        // Navigation Properties
        public User User { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}