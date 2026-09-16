using Project_Hrms.Data;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Services.EmployeeService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext db;
        public DepartmentService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddDepartment(Department d)
        {
            var emps = db.Users.Count(x => x.DepartmentId == d.DepartmentId);
            var de = new Department()
            {
                DepartmentName = d.DepartmentName,
                NoOfEmployee = emps,
                Status = d.Status,
                CreatedAt = DateTime.Now.ToString(),
                CreatedBy = "Admin"
            };

            db.Departments.Add(de);
            db.SaveChanges();
        }

        public void DeleteDepartment(int id)
        {
            var del = db.Departments.Find(id);
            if (del != null)
            {
                db.Departments.Remove(del);
                db.SaveChanges();
            }
        }

        public List<Department> FetchDepartments()
        {
            var data = db.Departments.ToList();
            return data;
        }

        public Department findDepartmentById(int id)
        {
            var dep = db.Departments.Find(id);
            return dep;
        }

        public void UpdateDepartment(Department r)
        {
            r.ModifiedBy = "Admin";
            r.ModifiedAt = DateTime.Now.ToString();

            db.Departments.Update(r);
            db.SaveChanges();
        }
    }
}
