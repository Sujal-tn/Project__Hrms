using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface ITimesheetService
    {
        Task<List<Timesheet>> GetAllTimesheets();
        Task<Timesheet> GetTimesheetById(int id);
        Task ApproveTimesheet(int id, string approvedBy);
        Task RejectTimesheet(int id, string approvedBy);
    }
}
