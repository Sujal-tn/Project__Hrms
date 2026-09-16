using System.ComponentModel.DataAnnotations;

namespace Project_Hrms.Models.EmployeeModel
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        public string? RoleName { get; set; }

        public string? Status { get; set; }

        public string? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public string? ModifiedAt { get; set; }

        public List<User>? Employes { get; set; }
    }
}
