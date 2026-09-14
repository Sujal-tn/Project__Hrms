using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models.EmployeeModel
{
    public class User
    {
      [Key]
    public int UserId { get; set; }
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? PhoneNumber { get; set; }

    [ForeignKey("RoleId")]
    public int RoleId { get; set; }
    public Role? Role { get; set; }

    [ForeignKey("DepartmentId")]
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    [ForeignKey("DesignationtId")]
    public int DesignationtId { get; set; }
    public Designation? Designation { get; set; }

    public string? DateOfJoining { get; set; }
    public string? DateOfBirth { get; set; }
    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string? AboutEmployee { get; set; }
    public string? ProfilePicture { get; set; }

    public string? ReportingManager { get; set; }
    public string? CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public int ModifiedBy { get; set; }
    public string? ModifiedAt { get; set; }
    public string? Status { get; set; }

        public string? RememberMe { get; set; }

    }
}
