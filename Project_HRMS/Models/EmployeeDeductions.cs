using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class EmployeeDeductions
    {
        [Key]
        public int EmployeeDeductionId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DeductionAmount { get; set; }

        [ForeignKey("EmployeeSalaries")]
        public int SalaryId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Deduction")]
        public int DeductionId { get; set; }

        public virtual EmployeeSalaries EmployeeSalaries { get; set; }
        public virtual Deduction Deduction { get; set; }

    }
}
