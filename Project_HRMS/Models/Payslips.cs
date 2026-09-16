using Project_Hrms.Models.EmployeeModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class Payslips
    {
        [Key]
        public int PayslipId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public string Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string PayslipPath { get; set; }

        [Required]
        public DateTime GeneratedOn { get; set; }

    }
}
