using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        [Required]
        [StringLength(50)]
        public string Priority { get; set; }

        [StringLength(255)]
        public string? FilePath { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Projects Project { get; set; }

        public List<TaskBoards> TaskBoards { get; set; }

        public List<TaskMembers> Taskmember { get; set; }

    }
}
