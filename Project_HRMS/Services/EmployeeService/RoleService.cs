using Project_Hrms.Data;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Services.EmployeeService
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext db;
        public RoleService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddRole(Role r)
        {
            var ro = new Role()
            {
                RoleName = r.RoleName,
                Status = r.Status,
                CreatedAt = DateTime.Now.ToString(),
                CreatedBy = "Admin",
              
            };

          db.Roles.Add(ro);
            db.SaveChanges();
        }

        public void DeleteRole(int id)
        {

            var data= db.Roles.Find(id);
            if(data!=null)
            {
                db.Roles.Remove(data);
                db.SaveChanges();
            }
          
        }

        public List<Role> FetchRoles()
        {

            var roles= db.Roles.ToList();
            return roles;
        }

        public Role findRoleById(int id)
        {

            var d = db.Roles.Find(id);
            if(d!=null)
            {
                return d;
            }
            else
            {
                return null;
            }
        }

        public void UpdateRole(Role r)
        {
            
            db.Roles.Update(r);
            db.SaveChanges();

        }
    }
}
