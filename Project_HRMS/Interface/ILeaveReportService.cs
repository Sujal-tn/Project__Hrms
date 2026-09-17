using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface ILeaveReportService
    {
        Task<List<LeaveRequest>> FeatchLeaveRequest();
    }
}
