using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }

        [ForeignKey("Projects")]
        public int ProjectId { get; set; }

        public Projects Projects { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Priority { get; set; }

        public string? FilePath { get; set; }

        public DateTime Deadline { get; set; }

        public List<TaskBoards> TaskBoards { get; set; }

        public List<TaskMembers> Taskmember { get; set; }
    }
}