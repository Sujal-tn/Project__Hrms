using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext db;
        public AttendanceService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Attendance>> GetAllAttendanceAsync()
        {
            var data = await db.Attendance
                .Include(a => a.User)
                .ThenInclude(u => u.Department)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
            return data;
        }

        public async Task<List<Attendance>> GetTodayAttendanceAsync()
        {
            var today = DateTime.Today;
            var data = await db.Attendance
                .Include(a => a.User)
                .Where(a => a.Date == today).ToListAsync();
            return data;
        }

        public async Task<Attendance> GetAttendanceByIdAsync(int id)
        {
            var data = await db.Attendance
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AttendanceId == id);
            return data;
        }

        public async Task UpdateAttendanceAsync(Attendance model)
        {
            var data = await db.Attendance.FindAsync(model.AttendanceId);
            if (data != null)
            {
                data.Date = model.Date;
                data.Status = model.Status;
                data.CheckIn = model.CheckIn;
                data.CheckOut = model.CheckOut;
                data.BreakHours = model.BreakHours;
                data.Late = model.Late;
                data.ProductionHours = model.ProductionHours;
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            var data = await db.Department.ToListAsync();
            return data;
        }

        public async Task<List<User>> GetAllEmployeesAsync()
        {
            var data = await db.User
                .Include(u => u.Department)
                .Where(u => u.Role.RoleName == "Employee")
                .ToListAsync();
            return data;
        }

        public async Task<List<int>> GetApprovedLeaveUserIdsForTodayAsync()
        {
            var today = DateTime.Today;
            var data = await db.LeaveRequest
                .Where(l => l.Status == "Approved" && l.StartDate <= today && l.EndDate >= today)
                .Select(l => l.UserId).ToListAsync();
            return data;
        }
    }
}
