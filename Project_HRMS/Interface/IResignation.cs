using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface
{
    public interface IResignation
    {
        Task AddResignation(Resignation r);
        Task<List<ResignationViewModels>> GetAllResignations();
        Task<Resignation?> FindResignationById(int id);
        Task UpdateResignation(Resignation r);
        Task DeleteResignation(int id);
        Task<List<User>> FetchUsers();
        Task<List<Department>> FetchDepartments();
    }
}
