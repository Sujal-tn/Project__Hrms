using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface ITimesheetService
    {
        //Admin Timesheet
        Task<List<Timesheet>> GetAllTimesheets();
        Task<Timesheet> GetTimesheetById(int id);
        Task ApproveTimesheet(int id, string approvedBy);
        Task RejectTimesheet(int id, string approvedBy);

        //Employee Timesheet
        Task<List<Projects>> GetAllProjectsAsync();
        Task<List<Timesheet>> GetTimesheetsByUserAsync(int userId, string filter);
        Task AddTimesheetAsync(Timesheet timesheet);
        Task SendForApprovalAsync(List<int> timesheetIds, int userId);
    }
}
