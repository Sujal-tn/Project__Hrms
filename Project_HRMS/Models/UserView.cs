using Project_Hrms.Models.EmployeeModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class UserView
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }

        public int RoleId { get; set; }
        public Role? Role { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public int? DesignationtId { get; set; }
        public Designation? Designation { get; set; }

        public string? DateOfJoining { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }

        public string? Address { get; set; }

        public string? AboutEmployee { get; set; }
        public IFormFile? ProfilePicture { get; set; }

        public string? ProfilePicturePath { get; set; }

        public int? ReportingManager { get; set; }

        public User? Manager { get; set; }
        public string? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedAt { get; set; }
        public string? Status { get; set; }
        public int Projects { get; set; }
        public int Done { get; set; }
        public int Progress { get; set; }

        public double    Productivity { get; set; }

        public BankInformation? BankInformation { get; set; }

        public FamilyInformation? FamilyInformation { get; set; }

        public EductionDetails? EductionDetails { get; set; }
        public Experince? Experince { get; set; }
    }
}
