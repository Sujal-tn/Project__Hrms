using Project_Hrms.Data;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Services.EmployeeService
{
    public class DesignationService : IDesignationService
    {
        private readonly ApplicationDbContext db;
        public DesignationService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddDesignation(Designation d)
        {

            var Des = new Designation()
            {
                DesignationName=d.DesignationName,
                DepartmentId=d.DepartmentId,
                Status=d.Status,
                CreatedAt=DateTime.Now.ToString(),
                CreatedBy="Admin"
            };
            db.Designations.Add(Des);
            db.SaveChanges();
        }

        public void DeleteDesignation(int id)
        {
            var data= db.Designations.Find(id);
            if(data!=null)
            {
                db.Designations.Remove(data);
                db.SaveChanges();
            }

        }

        public List<Department> fetchDepartments()
        {
            var departments = db.Departments
                       .Where(x => x.Status == "Active").ToList();
            return departments;

        }

        public List<Designation> FetchDesignation()
        {

            var desi = db.Designations.ToList();
            return desi;
        }

        public Designation findDesignationById(int id)
        {
           var di= db.Designations.Find(id);
            return di;

        }

        public void UpdateDesignation(Designation r)
        {
            db.Designations.Update(r);
            db.SaveChanges();

        }
    }
}
