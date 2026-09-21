using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface IPaySlipsReportService
    {
        Task<List<Payslips>> FeatchPaySlips();

        Task<List<Payslips>> GetSalaryGraphData();

        Task<byte[]> ExportToPDF();

        Task<byte[]> ExportToExcel();
    }
}