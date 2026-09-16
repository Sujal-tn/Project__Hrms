using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models.EmployeeModel
{
    public class Designation
    {

        [Key]
        public int DesignationId { get; set; }

        [ForeignKey("DepartmentId")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public string? DesignationName { get; set; }

        public int? NoOfEmployee { get; set; }

        public string? Status { get; set; }
        public string? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public string? ModifiedAt { get; set; }

        public List<User>? Employes { get; set; }
    }
}
