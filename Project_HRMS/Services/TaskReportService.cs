using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Services
{
    public class TaskReportService : ITaskReportService
    {
        private readonly ApplicationDbContext db;

        public TaskReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Tasks>> FetchTaskReport()
        {
            var data = await db.Tasks
                .Include(t => t.Projects)
                .Include(t => t.Taskmember)
                    .ThenInclude(tm => tm.User)
                .ToListAsync();

            return data;
        }
    }
}