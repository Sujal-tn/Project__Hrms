using Project_Hrms.Models.EmployeeModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class LeaveBalance
    {
        [Key]
        public int LeaveBalanceId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("DepartmentLeaves")]
        public int DepartmentLeavesId { get; set; }
        public DepartmentLeaves DepartmentLeaves { get; set; }

        [ForeignKey("MasterLeaveType")]
        public int LeaveTypeId { get; set; }
        public MasterLeaveType MasterLeaveType { get; set; }

        public int TotalLeaves { get; set; }

        public int UsedLeaves { get; set; }

        [NotMapped]
        public int RemainingLeaves
        {
            get
            {
                return TotalLeaves - UsedLeaves;
            }
        }

        public void ApplyLeave(int numberOfDays)
        {
            if (numberOfDays <= 0)
            {
                throw new ArgumentException("Number of leave days must be greater than zero.");
            }

            if (RemainingLeaves < numberOfDays)
            {
                throw new InvalidOperationException("Insufficient leave balance.");
            }

            UsedLeaves += numberOfDays;
        }

        public void RevertLeave(int numberOfDays)
        {
            if (numberOfDays <= 0)
            {
                return;
            }

            UsedLeaves -= numberOfDays;

            if (UsedLeaves < 0)
            {
                UsedLeaves = 0;
            }
        }
    }
}
