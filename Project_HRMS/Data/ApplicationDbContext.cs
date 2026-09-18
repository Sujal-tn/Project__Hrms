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
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<Payslips> Payslips { get; set; }
        public DbSet<MasterLeaveType> MasterLeaveType { get; set; }
        public DbSet<LeaveRequest> LeaveRequest { get; set; }
        public DbSet<LeaveBalance> LeaveBalance { get; set; }
        public DbSet<DepartmentLeaves> DepartmentLeaves { get; set; }

        public DbSet<Projects> Projects { get; set; }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<TaskBoards> TaskBoards { get; set; }

        public DbSet<TaskMembers> TaskMembers { get; set; }
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
        }
    }
}