using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Project_Hrms.Models
{
    public class TaskMembers
    {
        [Key]
        public int AssignedId { get; set; }

        [ForeignKey("Task")]
        public int? TaskId { get; set; }
        [AllowNull]
        public Tasks Task { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
