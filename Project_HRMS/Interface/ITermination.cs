using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface
{
    public interface ITermination
    {
        Task<List<TerminationViewModels>> GetAllTerminations();
        Task<Termination?> FindTerminationById(int id);
        Task AddTermination(Termination t);
        Task UpdateTermination(Termination t);
        Task DeleteTermination(int id);
        Task<List<User>> FetchUsers();
    }
}
