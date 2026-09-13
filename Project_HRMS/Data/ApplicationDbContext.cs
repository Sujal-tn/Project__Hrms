using Microsoft.EntityFrameworkCore;
using Project_Hrms.Models;

namespace Project_Hrms.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Timesheet> Timesheet { get; set; }
        public DbSet<TaskBoards> TaskBoards { get; set; }
        public DbSet<TaskMembers> TaskMembers { get; set; }
        public DbSet<MasterLeaveType> MasterLeaveType { get; set; }
        public DbSet<DepartmentLeaves> DepartmentLeaves { get; set; }
        public DbSet<LeaveBalance> LeaveBalance { get; set; }
        public DbSet<LeaveRequest> LeaveRequest { get; set; }
        public DbSet<Earning> Earning { get; set; }
        public DbSet<EarningType> EarningType { get; set; }
        public DbSet<Deduction> Deduction { get; set; }
        public DbSet<DeductionType> DeductionType { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<EmployeeBankDetails> EmployeeBankDetails { get; set; }
        public DbSet<EmployeeDeductions> EmployeeDeductions { get; set; }
        public DbSet<EmployeeEarnings> EmployeeEarnings { get; set; }
        public DbSet<EmployeeSalaries> EmployeeSalaries { get; set; }
        public DbSet<Payslips> Payslips { get; set; }
    }
}
