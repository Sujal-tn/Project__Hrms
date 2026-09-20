using Project_Hrms.Models;

namespace Project_Hrms.Interface.PayrollInterface
{
    public interface IEarningTypeService
    {
        Task<List<EarningType>> GetAllEarningTypesAsync();
        Task<EarningType?> GetEarningTypeByIdAsync(int id);
        Task AddEarningTypeAsync(EarningType earningType);
        Task UpdateEarningTypeAsync(EarningType earningType);
        Task<bool> DeleteEarningTypeAsync(int id);
    }
}
