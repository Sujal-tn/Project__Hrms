using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface.PayrollInterface
{
    public interface IEarningService
    {
        Task<List<Earning>> GetAllEarningsAsync();
        Task<Earning?> GetEarningByIdAsync(int id);
        Task AddEarningAsync(Earning earning);
        Task UpdateEarningAsync(Earning earning);
        Task<bool> DeleteEarningAsync(int id);

        Task<List<Earning>> GetEarningsByDeptDesignationAsync(int departmentId, int designationId);

        Task<List<Department>> GetDepartmentsAsync();
        Task<List<Designation>> GetDesignationsAsync();
    }
}
