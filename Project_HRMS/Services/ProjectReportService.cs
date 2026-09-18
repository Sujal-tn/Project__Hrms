using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Services
{
    public class ProjectReportService : IProjectReportService
    {
        private readonly ApplicationDbContext db;

        public ProjectReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Projects>> FetchProjectReport()
        {
            var data = await db.Projects.ToListAsync();

            return data;
        }
    }
}