using Project_Hrms.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoHRMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

    }
}
