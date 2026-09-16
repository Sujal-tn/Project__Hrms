using Project_Hrms.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Projects> Projects { get; set; }

        public DbSet<Trainers> Trainers { get; set; }

        public DbSet<Trainings> Trainings { get; set; }

        public DbSet<TrainingType> TrainingTypes { get; set; }
    }
}