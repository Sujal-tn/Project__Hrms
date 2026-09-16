using Microsoft.EntityFrameworkCore;
using Project_Hrms.Models.EmployeeModel;
using Project_Hrms.Models;

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

        public DbSet<User> Employees { get; set; }

        public DbSet<Trainers> Trainers { get; set; }

        public DbSet<Trainings> Trainings { get; set; }

        public DbSet<TrainingType> TrainingTypes { get; set; }

        public DbSet<Attendance> Attendance { get; set; }

        public DbSet<MasterLeaveType> MasterLeaveType { get; set; }

        public DbSet<LeaveRequest> LeaveRequest { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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

            base.OnModelCreating(modelBuilder);
        }
    }
}