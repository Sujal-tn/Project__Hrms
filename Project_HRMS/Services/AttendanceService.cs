using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;


namespace Project_Hrms.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext db;
        public AttendanceService(ApplicationDbContext db)
        {
            this.db = db;
        }
        //Admin Attendance
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
            var tomorrow = today.AddDays(1);
            var data = await db.Attendance
                .Include(a => a.User)
                .Where(a => a.Date >= today && a.Date < tomorrow).ToListAsync();
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
            var data = await db.Departments.ToListAsync();
            return data;
        }

        public async Task<List<User>> GetAllEmployeesAsync()
        {
            var data = await db.Users
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

        //Employee Attendance
        public async Task<Attendance?> GetTodayAttendanceAsync(int userId)
        {
            var today = DateTime.Today;
            return await db.Attendance
                .FirstOrDefaultAsync(a => a.UserId == userId && a.Date == today);
        }

        public async Task<string> GetAttendanceStatusAsync(int userId)
        {
            var attendance = await GetTodayAttendanceAsync(userId);

            if (attendance == null || attendance.CheckIn == null)
                return "Check-In";
            if (attendance.LunchIn == null)
                return "Lunch-In";
            if (attendance.LunchOut == null)
                return "Lunch-Out";
            if (attendance.CheckOut == null)
                return "Check-Out";

            return "Attendance Marked";
        }

        public async Task MarkAttendanceAsync(int userId)
        {
            var today = DateTime.Today;
            var attendance = await GetTodayAttendanceAsync(userId);
            var standardCheckOut = today.AddHours(18);

            if (attendance == null)
            {
                attendance = new Attendance
                {
                    Date = today,
                    UserId = userId,
                    CheckIn = DateTime.Now,
                    Status = "Present"
                };
                await db.Attendance.AddAsync(attendance);
            }
            else if (attendance.LunchIn == null)
            {
                attendance.LunchIn = DateTime.Now;
                db.Attendance.Update(attendance);
            }
            else if (attendance.LunchOut == null)
            {
                attendance.LunchOut = DateTime.Now;
                db.Attendance.Update(attendance);
            }
            else if (attendance.CheckOut == null)
            {
                attendance.CheckOut = DateTime.Now;

                decimal workingHours = 0, breakHours = 0, overtimeHours = 0, productionHours = 0;
                int lateMinutes = 0;

                if (attendance.CheckIn.HasValue)
                {
                    var totalWorkTime = attendance.CheckOut.Value - attendance.CheckIn.Value;

                    if (attendance.LunchIn.HasValue && attendance.LunchOut.HasValue)
                    {
                        breakHours = Math.Round((decimal)(attendance.LunchOut.Value - attendance.LunchIn.Value).TotalHours, 1);
                    }

                    workingHours = Math.Round((decimal)totalWorkTime.TotalHours - breakHours, 1);
                    productionHours = workingHours - breakHours;

                    if (attendance.CheckOut.Value > standardCheckOut)
                    {
                        overtimeHours = Math.Round((decimal)(attendance.CheckOut.Value - standardCheckOut).TotalHours, 1);
                    }

                    var standardCheckIn = today.AddHours(9);
                    if (attendance.CheckIn.Value > standardCheckIn)
                    {
                        lateMinutes = (int)(attendance.CheckIn.Value - standardCheckIn).TotalMinutes;
                    }
                }

                attendance.WorkingHours = workingHours;
                attendance.BreakHours = breakHours;
                attendance.OvertimeHours = overtimeHours;
                attendance.ProductionHours = productionHours;
                attendance.Late = lateMinutes;
                attendance.Status = workingHours <= 4.0m ? "Half Day" : "Present";

                db.Attendance.Update(attendance);
            }

            await db.SaveChangesAsync();
        }

        public async Task<List<Attendance>> GetAttendanceHistoryAsync(int userId)
        {
            return await db.Attendance
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<List<Attendance>> GetFilteredAttendanceAsync(int userId, string? status, DateTime? startDate, DateTime? endDate, string sortBy)
        {
            var query = db.Attendance.Where(a => a.UserId == userId).AsQueryable();

            if (!string.IsNullOrEmpty(status) && status != "All")
            {
                query = query.Where(a => a.Status == status);
            }

            if (startDate.HasValue)
            {
                query = query.Where(a => a.Date >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                query = query.Where(a => a.Date <= endDate.Value.Date);
            }

            query = sortBy switch
            {
                "asc" => query.OrderBy(a => a.Date),
                "last7Days" => query.Where(a => a.Date >= DateTime.Today.AddDays(-7)).OrderByDescending(a => a.Date),
                "lastMonth" => query.Where(a => a.Date >= DateTime.Today.AddMonths(-1)).OrderByDescending(a => a.Date),
                _ => query.OrderByDescending(a => a.Date)
            };

            return await query.ToListAsync();
        }

        public async Task<(decimal TotalToday, decimal TotalWeek, decimal TotalMonth, decimal OvertimeMonth)> GetAttendanceSummaryAsync(int userId)
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(6);
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var totalToday = await db.Attendance
                .Where(a => a.UserId == userId && a.Date == today)
                .SumAsync(a => a.WorkingHours);

            var totalWeek = await db.Attendance
                .Where(a => a.UserId == userId && a.Date >= startOfWeek && a.Date <= endOfWeek)
                .SumAsync(a => a.WorkingHours);

            var totalMonth = await db.Attendance
                .Where(a => a.UserId == userId && a.Date >= startOfMonth && a.Date <= endOfMonth)
                .SumAsync(a => a.WorkingHours);

            var overtimeMonth = await db.Attendance
                .Where(a => a.UserId == userId && a.Date >= startOfMonth && a.Date <= endOfMonth)
                .SumAsync(a => a.OvertimeHours);

            return (totalToday, totalWeek, totalMonth, overtimeMonth);
        }
    }
}