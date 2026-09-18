using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface IProjectReportService
    {
        Task<List<Projects>> FetchProjectReport();
    }
}