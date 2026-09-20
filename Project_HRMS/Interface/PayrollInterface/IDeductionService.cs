using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface.PayrollInterface
{
    public interface IDeductionService
    {
        Task<List<Deduction>> GetAllDeductionsAsync();
        Task<Deduction?> GetDeductionByIdAsync(int id);
        Task AddDeductionAsync(Deduction deduction);
        Task UpdateDeductionAsync(Deduction deduction);
        Task<bool> DeleteDeductionAsync(int id);

        Task<List<Deduction>> GetDeductionsByDeptDesignationAsync(int departmentId, int designationId);

        Task<List<Department>> GetDepartmentsAsync();
        Task<List<Designation>> GetDesignationsAsync();
    }
}
