using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Services
{
    public class DailyReportService : IDailyReportService
    {
        private readonly ApplicationDbContext db;

        public DailyReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<DailyReportModel>> FeatchDailyAttendace()
        {
            var data = await db.Attendance
                .Select(a => new DailyReportModel
                {
                    Name = a.User.FirstName + " " + a.User.LastName,
                    Date = a.Date,
                    Department = a.User.Department.DepartmentName,
                    Status = a.Status
                })
                .ToListAsync();

            return data;
        }

    }
}