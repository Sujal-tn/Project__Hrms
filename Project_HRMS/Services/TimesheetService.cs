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
    }
}
