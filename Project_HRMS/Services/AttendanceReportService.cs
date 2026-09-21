using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Services
{
    public class AttendanceReportService : IAttendanceReport
    {
        private readonly ApplicationDbContext db;

        public AttendanceReportService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<Attendance>> FeatchAttendanceReport()
        {
            var data = await db.Attendance
                .Include(x => x.User)
                .ToListAsync();
            return data;
        }
    }
}
