using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface IMasterEvent
    {
        Task AddNewMasterEvent(MasterEvents e);

        Task<List<MasterEvents>> GetAllMasterEvents();

        Task DeleteMasterEvent(int id);
    }
}
