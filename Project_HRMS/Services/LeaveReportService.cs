using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Services
{
    public class LeaveReportService : ILeaveReportService
    {
        private readonly ApplicationDbContext db;

        public LeaveReportService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<LeaveRequest>> FeatchLeaveRequest()
        {
            var data = await db.LeaveRequest
                .Include(x => x.User ).Include(e => e.MasterLeaveType)
                .ToListAsync();
            return data;
        }
    }
}
