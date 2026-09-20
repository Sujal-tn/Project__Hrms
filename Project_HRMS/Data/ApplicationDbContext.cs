using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Trainers> Trainers { get; set; }
        public DbSet<Trainings> Trainings { get; set; }
        public DbSet<TrainingType> TrainingTypes { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<Resignation> Resignation { get; set; }
        public DbSet<Termination> Termination { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<Payslips> Payslips { get; set; }
        public DbSet<MasterLeaveType> MasterLeaveType { get; set; }
        public DbSet<LeaveRequest> LeaveRequest { get; set; }
        public DbSet<LeaveBalance> LeaveBalance { get; set; }
        public DbSet<DepartmentLeaves> DepartmentLeaves { get; set; }

        public DbSet<Earning> Earnings { get; set; }
        public DbSet<EarningType> EarningTypes { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<DeductionType> DeductionTypes { get; set; }
        public DbSet<EmployeeSalaries> EmployeeSalaries { get; set; }
        public DbSet<EmployeeEarnings> EmployeeEarnings { get; set; }
        public DbSet<EmployeeDeductions> EmployeeDeductions { get; set; }
        public DbSet<EmployeeBankDetails> EmployeeBankDetails { get; set; }

        public DbSet<Projects> Projects { get; set; }

        public DbSet<AdminAddDocumentsName> AdminAddDocumentsNames { get; set; }

        public DbSet<EmployeeAddDocumentsName> EmployeeAddDocumentsNames { get; set; }

        public DbSet<FileUploads> Files { get; set; }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<TaskBoards> TaskBoards { get; set; }

        public DbSet<TaskMembers> TaskMembers { get; set; }

        public DbSet<Events> Events { get; set; }

        public DbSet<MasterEvents> MasterEvents { get; set; }
<<<<<<< HEAD
<<<<<<< HEAD

        public DbSet<UploadDocuments> MasterDocument { get; set; }


=======
>>>>>>> 7b3b1cadb92f4eda1bfe6b1bd61e9eec49ac4aed
=======

        public DbSet<BankInformation> BankInformations { get; set; }

        public DbSet<FamilyInformation> FamilyInformations { get; set; }

        public DbSet<EductionDetails> EductionDetails { get; set; }

        public DbSet<Experince> Experinces { get; set; }
>>>>>>> origin/main
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Designation>(
                d => d.HasOne(x => x.Department)
                      .WithMany(x => x.Designations)
                      .HasForeignKey(x => x.DepartmentId)
                      .OnDelete(DeleteBehavior.Restrict)
            );

            modelBuilder.Entity<User>(
                e =>
                {
                    e.HasOne(x => x.Role)
                     .WithMany(x => x.Employes)
                     .HasForeignKey(x => x.RoleId)
                     .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Department)
                     .WithMany(x => x.Employes)
                     .HasForeignKey(x => x.DepartmentId)
                     .OnDelete(DeleteBehavior.Restrict);

                    e.HasOne(x => x.Designation)
                     .WithMany(x => x.Employes)
                     .HasForeignKey(x => x.DesignationtId)
                     .OnDelete(DeleteBehavior.Restrict);

                  

                }
            );

            modelBuilder.Entity<DepartmentLeaves>()
                .HasOne(dl => dl.MasterLeaveType)
                .WithMany(mlt => mlt.DepartmentLeaves)
                .HasForeignKey(dl => dl.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // EmployeeSalaries -> User
            modelBuilder.Entity<EmployeeSalaries>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            // EmployeeEarnings -> EmployeeSalaries
            modelBuilder.Entity<EmployeeEarnings>()
                .HasOne(x => x.EmployeeSalaries)
                .WithMany(x => x.EmployeeEarnings)
                .HasForeignKey(x => x.SalaryId)
                .OnDelete(DeleteBehavior.Cascade);

            // EmployeeEarnings -> User
            modelBuilder.Entity<EmployeeEarnings>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // EmployeeEarnings -> Earning
            modelBuilder.Entity<EmployeeEarnings>()
                .HasOne(x => x.Earning)
                .WithMany()
                .HasForeignKey(x => x.EarningId)
                .OnDelete(DeleteBehavior.Cascade);


            // EmployeeDeductions -> EmployeeSalaries
            modelBuilder.Entity<EmployeeDeductions>()
                .HasOne(x => x.EmployeeSalaries)
                .WithMany(x => x.EmployeeDeductions)
                .HasForeignKey(x => x.SalaryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeeDeductions>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // EmployeeDeductions -> Deduction
            modelBuilder.Entity<EmployeeDeductions>()
                .HasOne(x => x.Deduction)
                .WithMany()
                .HasForeignKey(x => x.DeductionId)
                .OnDelete(DeleteBehavior.Cascade);

            // EmployeeBankDetails -> User
            modelBuilder.Entity<EmployeeBankDetails>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskBoards>()
                .HasOne(x => x.Project)
                .WithMany()
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TaskBoards>()
                .HasOne(x => x.Task)
                .WithMany(x => x.TaskBoards)
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Projects>()
                .HasOne(x => x.User)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BankInformation>()
               .HasOne(x => x.User)
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FamilyInformation>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EductionDetails>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Experince>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}