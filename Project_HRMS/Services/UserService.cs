using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Interface.EmployeeInterface;
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
        public async Task<List<User>> FeatchUser()
        {
            var data = await db.Users.ToListAsync();
            return data;
        }
    }
}
