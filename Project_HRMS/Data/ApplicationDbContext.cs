using Microsoft.EntityFrameworkCore;
using Project_Hrms.Models;

namespace Project_Hrms.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Designation)
                .WithMany()
                .HasForeignKey(u => u.DesignationtId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Designation>()
                .HasOne(d => d.Department)
                .WithMany(dep => dep.Designations)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskBoards>()
                .HasOne(t => t.Task)
                .WithMany()
                .HasForeignKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskBoards>()
                .HasOne(t => t.Project)
                .WithMany()
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DepartmentLeaves>()
                .HasOne(dl => dl.Department)
                .WithMany(dep => dep.DepartmentLeaves)
                .HasForeignKey(dl => dl.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DepartmentLeaves>()
                .HasOne(dl => dl.MasterLeaveType)
                .WithMany(mlt => mlt.DepartmentLeaves)
                .HasForeignKey(dl => dl.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveBalance>()
                .HasOne(lb => lb.User)
                .WithMany(u => u.LeaveBalances)
                .HasForeignKey(lb => lb.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveBalance>()
                .HasOne(lb => lb.DepartmentLeaves)
                .WithMany()
                .HasForeignKey(lb => lb.DepartmentLeavesId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveBalance>()
                .HasOne(lb => lb.MasterLeaveType)
                .WithMany(mlt => mlt.LeaveBalances)
                .HasForeignKey(lb => lb.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.User)
                .WithMany(u => u.LeaveRequests)
                .HasForeignKey(lr => lr.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.MasterLeaveType)
                .WithMany(mlt => mlt.LeaveRequests)
                .HasForeignKey(lr => lr.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Earning>()
                .HasOne(e => e.EarningType)
                .WithMany(et => et.Earnings)
                .HasForeignKey(e => e.EarntypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Earning>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Earnings)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Earning>()
                .HasOne(e => e.Designation)
                .WithMany(d => d.Earnings)
                .HasForeignKey(e => e.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Deduction>()
                .HasOne(d => d.DeductionType)
                .WithMany(dt => dt.Deductions)
                .HasForeignKey(d => d.DeductionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Deduction>()
                .HasOne(d => d.Department)
                .WithMany(dep => dep.Deductions)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Deduction>()
                .HasOne(d => d.Designation)
                .WithMany(des => des.Deductions)
                .HasForeignKey(d => d.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payslips>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeSalaries>()
                .HasOne(es => es.User)
                .WithMany()
                .HasForeignKey(es => es.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeEarnings>()
                .HasOne(ee => ee.User)
                .WithMany()
                .HasForeignKey(ee => ee.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeEarnings>()
                .HasOne(ee => ee.EmployeeSalaries)
                .WithMany(es => es.EmployeeEarnings)
                .HasForeignKey(ee => ee.SalaryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeEarnings>()
                .HasOne(ee => ee.Earning)
                .WithMany()
                .HasForeignKey(ee => ee.EarningId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeDeductions>()
                .HasOne(ed => ed.User)
                .WithMany()
                .HasForeignKey(ed => ed.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeDeductions>()
                .HasOne(ed => ed.EmployeeSalaries)
                .WithMany(es => es.EmployeeDeductions)
                .HasForeignKey(ed => ed.SalaryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeDeductions>()
                .HasOne(ed => ed.Deduction)
                .WithMany()
                .HasForeignKey(ed => ed.DeductionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeeBankDetails>()
                .HasOne(bd => bd.User)
                .WithMany()
                .HasForeignKey(bd => bd.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Projects> Projects { get; set; }

        public DbSet<Trainers> Trainers { get; set; }

        public DbSet<Trainings> Trainings { get; set; }

        public DbSet<TrainingType> TrainingTypes { get; set; }
    }
}