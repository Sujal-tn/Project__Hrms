using System.ComponentModel.DataAnnotations;

namespace Project_Hrms.Models.EmployeeModel
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public int NoOfEmployee { get; set; }

        public string? Status { get; set; }
        public string? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public string? ModifiedAt { get; set; }

        public List<User>? Employes { get; set; }

        public List<Designation>? Designations { get; set; }

        public List<DepartmentLeaves> DepartmentLeaves { get; set; }

    }
}
