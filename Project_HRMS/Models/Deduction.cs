using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class Deduction
    {
        [Key]
        public int DeductionId { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal DeductionPercentage { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; } 
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; } 

        [ForeignKey("DeductionType")]
        public int DeductionTypeId { get; set; } // Foreign Key to DeductionType
        public virtual DeductionType DeductionType { get; set; }


        public int DepartmentId { get; set; } // Foreign Key to Department
        public virtual Department Department { get; set; }


        public int DesignationId { get; set; } // Foreign Key to Designation
        public virtual Designation Designation { get; set; }

    }
}
