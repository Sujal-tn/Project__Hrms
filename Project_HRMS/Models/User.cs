using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Project_Hrms.Models
{
    [Table("User")]
    public class User
    {
        
            [Key]
            public int UserId { get; set; }

            public string? FirstName { get; set; }

            public string? LastName { get; set; }

            public string? Email { get; set; }

            public string? PasswordHash { get; set; }

            public string? PhoneNumber { get; set; }

            public int? RoleId { get; set; }

            public int? DepartmentId { get; set; }

            public int? DesignationtId { get; set; }

            public DateTime? DateOfJoining { get; set; }

            public DateTime? DateOfBirth { get; set; }

            public string? Gender { get; set; }

            public string? Address { get; set; }

            public string? AboutEmployee { get; set; }

            public string? ProfilePicture { get; set; }

            public int? RoleId1 { get; set; }

            public string? ReportingManager { get; set; }

            public DateTime? CreatedAt { get; set; }

            public string? CreatedBy { get; set; }

            public DateTime? ModifiedAt { get; set; }

            public string? ModifiedBy { get; set; }

            public string? Status { get; set; }

        //public string? DepartmentName { get; set; }
        public Role? Role { get; set; }
        public Department? Department { get; set; }
        public Designation? Designation { get; set; }

        public List<LeaveBalance> LeaveBalances { get; set; }
        public List<LeaveRequest> LeaveRequests { get; set; }
    }
}
