using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface ITaskReportService
    {
        Task<List<Tasks>> FetchTaskReport();
    }
}