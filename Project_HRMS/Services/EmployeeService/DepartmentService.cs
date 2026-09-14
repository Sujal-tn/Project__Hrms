using Project_Hrms.Data;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Services.EmployeeService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext db;
        public DepartmentService(AppDbContext db)
        {
            this.db = db;
        }
        public void AddDepartment(Department d)
        {
            throw new NotImplementedException();
        }

        public void DeleteDepartment(int id)
        {
            throw new NotImplementedException();
        }

        public List<Department> FetchRoles()
        {
            throw new NotImplementedException();
        }

        public Department findRoleById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateDepartment(Department r)
        {
            throw new NotImplementedException();
        }
    }
}
