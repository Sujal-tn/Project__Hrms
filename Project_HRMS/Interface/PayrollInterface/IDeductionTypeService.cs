using Project_Hrms.Models;

namespace Project_Hrms.Interface.PayrollInterface
{
    public interface IDeductionTypeService
    {
        Task<List<DeductionType>> GetAllDeductionTypesAsync();
        Task<DeductionType?> GetDeductionTypeByIdAsync(int id);
        Task AddDeductionTypeAsync(DeductionType deductionType);
        Task UpdateDeductionTypeAsync(DeductionType deductionType);
        Task<bool> DeleteDeductionTypeAsync(int id);
    }
}
