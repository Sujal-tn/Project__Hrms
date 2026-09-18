using Project_Hrms.Data;
using Project_Hrms.Models;
using Microsoft.EntityFrameworkCore;
using Project_Hrms.Interface;

namespace Project_Hrms.Services
{
    public class TimesheetService : ITimesheetService
    {
        private readonly ApplicationDbContext db;
        public TimesheetService(ApplicationDbContext db)
        {
            this.db = db;
        }
        //Admin Timesheet
        public async Task<List<Timesheet>> GetAllTimesheets()
        {
            var data = await db.Timesheets
                .Include(t => t.User)
                .Include(t => t.Projects)
                .ToListAsync();

            return data;
        }

        public async Task<Timesheet> GetTimesheetById(int id)
        {
            var data = await db.Timesheets
                .Include(t => t.User)
                .Include(t => t.Projects)
                .FirstOrDefaultAsync(t => t.TimesheetId == id);

            return data;
        }

        public async Task ApproveTimesheet(int id, string approvedBy)
        {
            var data = await db.Timesheets.FindAsync(id);
            if (data != null)
            {
                data.Status = "Approved";
                data.ApprovedBy = approvedBy;
                data.ApprovedAt = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }

        public async Task RejectTimesheet(int id, string approvedBy)
        {
            var data = await db.Timesheets.FindAsync(id);
            if (data != null)
            {
                data.Status = "Rejected";
                data.ApprovedBy = approvedBy;
                data.ApprovedAt = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }

        //Employee Timesheet
        public async Task<List<Projects>> GetAllProjectsAsync()
        {
            return await db.Projects.ToListAsync();
        }

        public async Task<List<Timesheet>> GetTimesheetsByUserAsync(int userId, string filter)
        {
            var query = db.Timesheets
                .Include(t => t.Projects)
                .Where(t => t.UserId == userId)
                .AsQueryable();

            var today = DateTime.Today;

            if (filter == "Weekly")
            {
                var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
                var endOfWeek = startOfWeek.AddDays(7);
                query = query.Where(t => t.Date >= startOfWeek && t.Date < endOfWeek);
            }
            else if (filter == "Monthly")
            {
                var startOfMonth = new DateTime(today.Year, today.Month, 1);
                var endOfMonth = startOfMonth.AddMonths(1);
                query = query.Where(t => t.Date >= startOfMonth && t.Date < endOfMonth);
            }
            else if (filter == "PendingApproval")
            {
                query = query.Where(t => t.Status == "Pending Approval");
            }

            return await query.OrderByDescending(t => t.Date).ToListAsync();
        }

        public async Task AddTimesheetAsync(Timesheet timesheet)
        {
            timesheet.Status = "Pending";
            timesheet.CreatedAt = DateTime.Now;

            await db.Timesheets.AddAsync(timesheet);
            await db.SaveChangesAsync();
        }

        public async Task SendForApprovalAsync(List<int> timesheetIds, int userId)
        {
            var timesheets = await db.Timesheets
                .Where(t => timesheetIds.Contains(t.TimesheetId) && t.UserId == userId)
                .ToListAsync();

            foreach (var timesheet in timesheets)
            {
                timesheet.Status = "Pending Approval";
            }

            db.Timesheets.UpdateRange(timesheets);
            await db.SaveChangesAsync();
        }
    }
}
