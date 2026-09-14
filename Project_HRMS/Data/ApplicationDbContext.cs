using Project_Hrms.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Projects> Projects { get; set; }
    }
}
