using Project_Hrms.Models.EmployeeModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class Deduction
    {
        [Key]
        public int DeductionId { get; set; } 

        [ForeignKey("DeductionType")]
        public int DeductionTypeId { get; set; } 
        public virtual DeductionType DeductionType { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; } 
        public virtual Department Department { get; set; }

        [ForeignKey("Designation")]
        public int DesignationId { get; set; } 
        public virtual Designation Designation { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal DeductionPercentage { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; } 

        public DateTime? ModifiedAt { get; set; }

        public string? ModifiedBy { get; set; }

    }
}
