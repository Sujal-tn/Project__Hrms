using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface
{
    public interface IUserService
    {
        Task<List<User>> FeatchUser();
    }
}
