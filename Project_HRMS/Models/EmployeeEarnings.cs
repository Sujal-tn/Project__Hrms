using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class EmployeeEarnings
    {
        [Key]
        public int EmployeeEarningId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal EarningAmount { get; set; }

        [ForeignKey("EmployeeSalaries")]
        public int SalaryId { get; set; }
        public EmployeeSalaries EmployeeSalaries { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Earning")]
        public int EarningId { get; set; }
        public Earning Earning { get; set; }

    }
}
