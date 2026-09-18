using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface.EmployeeInterface
{
    public interface IEmpService
    {
        void AddEmp(User d);
        List<User> FetchEmp();

        void DeleteEmp(int id);

        User findEmpById(int id);

        void UpdateEmp(User r);

        List<Department> fetchDepartments();

        List<Designation> fetchDesignation();

        List<Role> fetchRole();

        Task<List<User>> FetchManagersAsync();

        string GetRoleName(int roleId);
    }
}
