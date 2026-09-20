using Project_Hrms.Models;

namespace Project_Hrms.Interface.PayrollInterface
{
    public interface IEmployeeSalaryService
    {
        Task<List<EmployeeSalaries>> GetAllEmployeeSalariesAsync(string? designation, string? dateRange, string? sortBy);
        Task<EmployeeSalaries?> GetEmployeeSalaryByIdAsync(int id);
        Task<EmployeeSalaries?> GetLatestSalaryByUserIdAsync(int userId);

        Task<(List<Earning> Earnings, List<Deduction> Deductions)> GetApplicableRulesForUserAsync(int userId);

        Task<(List<Earning> Earnings, List<Deduction> Deductions)> GetAllActiveRulesAsync();

        Task AddEmployeeSalaryAsync(EmployeeSalaries salary, List<EmployeeEarnings> earnings, List<EmployeeDeductions> deductions);
        Task<bool> DeleteEmployeeSalaryAsync(int id);
    }
}
