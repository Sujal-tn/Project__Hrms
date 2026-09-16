using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;
        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public List<User> FeatchUser()
        {
            var data = db.Employees.ToList();
            return data;
        }
    }
}
