using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface.LoginInterface;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Services.LoginService
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext db;

        public AuthService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public User LoginUser(string email, string password)
        {

            var u = db.Employees.Include(x => x.Role)
                .FirstOrDefault(x => x.Email == email && x.Password == password && x.Status == "Active");
            return u;

        }
    }
}
