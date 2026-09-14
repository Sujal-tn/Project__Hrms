using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface.LoginInterface
{
    public interface IAuthService
    {
        User LoginUser(string email, string password);
    }
}
