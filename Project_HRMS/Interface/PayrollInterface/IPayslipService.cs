using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface.PayrollInterface
{
    public interface IPayslipService
    {
        Task<Payslips?> GetPayslipDataAsync(int userId, string month, int year);
        Task<bool> GeneratePayslipPdfAsync(int userId, string month, int year);
        Task<List<int>> GeneratePayslipsForSelectedUsersAsync(List<int> userIds, string month, int year);
        Task<List<Payslips>> GetTransactionHistoryAsync(string? month, int? year, int? departmentId, int? designationId);
        Task<bool> DeletePayslipAsync(int payslipId);
        Task<List<Department>> GetDepartmentsAsync();
        Task<List<Designation>> GetDesignationsAsync();
        Task<List<Payslips>> GetMyPayslipsAsync(int userId, string? month = null, int? year = null);
    }
}