using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface IPaySlipsReportService
    {
        Task<List<Payslips>> FeatchPaySlips();
    }
}
