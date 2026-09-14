using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface.EmployeeInterface
{
    public interface IDepartmentService
    {
        void AddDepartment(Department d);
        List<Department> FetchRoles();

        void DeleteDepartment(int id);

        Department findRoleById(int id);

        void UpdateDepartment(Department r);
    }
}
