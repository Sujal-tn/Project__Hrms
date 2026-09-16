using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface.EmployeeInterface
{
    public interface IRoleService
    {
        void AddRole(Role r);
        List<Role> FetchRoles();

        void DeleteRole(int id);

        Role findRoleById(int id);

        void UpdateRole(Role r);
    }
}
