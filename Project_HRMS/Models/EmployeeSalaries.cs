using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class EmployeeSalaries
    {
        [Key]
        public int SalaryId { get; set; }  

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalSalary { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal NetSalary { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<EmployeeEarnings> EmployeeEarnings { get; set; }
        public ICollection<EmployeeDeductions> EmployeeDeductions { get; set; }

    }
}
