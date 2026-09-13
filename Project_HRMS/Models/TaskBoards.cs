using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class TaskBoards
    {
        [Key]
        public int TaskBoardId { get; set; }

        [Range(0, 100)]
        public int Percentage { get; set; }

        [Required]
        public DateTime DueDate { get; set; }


        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public Projects Project { get; set; }

        [ForeignKey("Task")]
        public int TaskId { get; set; }

        public Tasks Task { get; set; }
    }
}