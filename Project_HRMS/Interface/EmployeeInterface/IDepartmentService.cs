using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface.EmployeeInterface
{
    public interface IDepartmentService
    {
        void AddDepartment(Department d);
        List<Department> FetchDepartments();

        void DeleteDepartment(int id);

        Department findDepartmentById(int id);

        void UpdateDepartment(Department r);
    }
}
