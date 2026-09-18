using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface IDailyReportService
    {
        Task<List<DailyReportModel>> FeatchDailyAttendace();
    }
}