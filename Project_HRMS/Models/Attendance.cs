using Project_Hrms.Models.EmployeeModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public DateTime? LunchIn { get; set; }
        public DateTime? LunchOut { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal WorkingHours { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal ProductionHours { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal OvertimeHours { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal BreakHours { get; set; }
        public int Late { get; set; }
        public string Status { get; set; }

    }
}
