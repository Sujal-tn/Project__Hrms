using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Services
{
    public class TerminationService : ITermination
    {
        private readonly ApplicationDbContext db;
        public TerminationService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<TerminationViewModels>> GetAllTerminations()
        {
            var data = await (from t in db.Termination join u in db.Users on t.UserId equals u.UserId
                              select new TerminationViewModels
                              {
                                  TerminationId = t.TerminationId,
                                  UserId = t.UserId,
                                  EmployeeName = u.FirstName + " " + u.LastName,
                                  ProfilePicture = u.ProfilePicture,
                                  TerminationType = t.TerminationType,
                                  NoticeDate = t.NoticeDate,
                                  ResignDate = t.ResignDate,
                                  TerminationDate = t.ResignDate.ToString("dd MM yyyy"),
                                  Reason = t.Reason
                              }).ToListAsync();
            return data;
        }
        public async Task<Termination> FindTerminationById(int id)
        {
            var data = await db.Termination.FindAsync(id);
            return data;
        }
        public async Task AddTermination(Termination t)
        {
            await db.Termination.AddAsync(t);
            await db.SaveChangesAsync();
        }
        public async Task UpdateTermination(Termination t)
        {
            db.Termination.Update(t);
            await db.SaveChangesAsync();
        }
        public async Task DeleteTermination(int id)
        {
            var data = await db.Termination.FindAsync(id);
            if (data != null)
            {
                db.Termination.Remove(data);
                await db.SaveChangesAsync();
            }
        }
        public async Task<List<User>> FetchUsers()
        { 
            return await db.Users.ToListAsync(); 
        }
    }
}

