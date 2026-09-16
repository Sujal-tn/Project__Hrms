using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class Timesheet
    {
        [Key]
        public int TimesheetId { get; set; }
        public DateTime Date { get; set; }
        public int WorkHours { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Projects")]
        public int ProjectId { get; set; }
        public Projects Projects { get; set; }


    }
}
