using Project_Hrms.Models.EmployeeModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class Payslips
    {
        [Key]
        public int PayslipId { get; set; }

        [Required]
        public string Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public string OrganizationName { get; set; }

        public string OrganizationAddress { get; set; }

        public string OrganizationEmail { get; set; }

        public string OrganizationPhone { get; set; }

        public string? PayslipPath { get; set; }

        public DateTime GeneratedOn { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal TotalHoursInMonth { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal WorkedHours { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal HourlyRate { get; set; }

        public List<Earning> Earnings { get; set; } = new();

        public List<Deduction> Deductions { get; set; } = new();

        [Column(TypeName = "decimal(9,2)")]
        public decimal TotalEarnings { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal TotalDeductions { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal NetSalary { get; set; }
    }
}