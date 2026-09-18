using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;
using System.Security.Cryptography;
using System.IO;

namespace Project_Hrms.Services.EmployeeService
{
    public class EmpService : IEmpService
    {

        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment env;

        public EmpService(ApplicationDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public void AddEmp(UserView d)
        {
            string path = env.WebRootPath;
            string filepath = "Content/Images" + d.ProfilePicture.FileName;
            string fullpath =Path.Combine(path, filepath);
            UploadFile(d.ProfilePicture, fullpath);

            var user = new User()
            {
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                Password = d.Password,
                RoleId = d.RoleId,
                DepartmentId = d.DepartmentId,
                DesignationtId = d.DesignationtId,
                ReportingManager = d.ReportingManager,
                ProfilePicture = filepath,
                PhoneNumber = d.PhoneNumber,
                Gender = d.Gender,
                Status = d.Status,
                Address = d.Address,
                DateOfBirth = d.DateOfBirth,
                DateOfJoining = d.DateOfJoining,
                AboutEmployee = d.AboutEmployee,
                CreatedAt = DateTime.Now.ToString(),
                CreatedBy = "Admin",

            };

            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            stream.Close();
        }
        public void DeleteEmp(int id)
        {
            var data = db.Users.Find(id);
            if (data != null)
            {
                db.Users.Remove(data);
                db.SaveChanges();
            }
        }

        public List<Department> fetchDepartments()
        {
            var departments = db.Departments.Where(x => x.Status == "Active").ToList();
            return departments;
        }

        public List<Designation> fetchDesignation()
        {
            var desig = db.Designations.Where(x => x.Status == "Active").ToList();
            return desig;

        }

        public List<User> FetchEmp()
        {
            var emps = db.Users.Include(x => x.Designation)
             .Include(x => x.Department)
             .Include(x => x.Role)
             .Include(x => x.Manager).ToList();
            return emps;
        }

        public async Task<List<User>> FetchManagersAsync()
        {

            return await db.Users
                .Include(x => x.Role)
                .Where(x => x.Role != null && x.Role.RoleName == "Manager").ToListAsync();
        }

        public List<Role> fetchRole()
        {

            var roles = db.Roles.Where(x => x.Status == "Active").ToList();
            return roles;

        }

        public User findEmpById(int id)
        {
            var e = db.Users
                .Include(x => x.Role)
        .Include(x => x.Department)
        .Include(x => x.Designation)
        .Include(x => x.Manager)
        .FirstOrDefault(x => x.UserId == id); 
            return e;


        }

        public string GetRoleName(int roleId)
        {

            var rname = db.Roles
               .Where(x => x.RoleId == roleId)
               .Select(x => x.RoleName)
               .FirstOrDefault();
            return rname;
        }

        public void UpdateEmp(User r)
        {

            db.Users.Update(r);
            db.SaveChanges();
        }
    }
}
