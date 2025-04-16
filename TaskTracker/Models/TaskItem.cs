using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Models
{
    public class TaskItem
    {
        public int TaskItemId { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Status { get; set; } = "Pending";

        public DateTime DueDate { get; set; }

        public int ProjectId { get; set; }

        public Project? Project { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Address { get; set; }


    }
}
