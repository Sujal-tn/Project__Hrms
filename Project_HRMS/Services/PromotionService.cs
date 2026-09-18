using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Services
{
    public class PromotionService : IPromotion

    {
        private readonly ApplicationDbContext db;
        public PromotionService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<PromotionViewModels>> GetAllPromotions()
        {
            var data = await (from p in db.Promotion join u in db.Users on p.UserId equals u.UserId
                              select new PromotionViewModels
                              {
                                  PromotionId = p.PromotionId,
                                  UserId = p.UserId,
                                  EmployeeName = u.FirstName + " " + u.LastName,
                                  DesignationFrom = p.DesignationFrom,
                                  DesignationTo = p.DesignationTo,
                                  Date = p.Date,
                                  PromotionDate = p.Date.ToString("dd MM yyyy")
                              }).ToListAsync();
            return data;
        }
        public async Task<Promotion?> FindPromotionById(int id)
        {
            var data = await db.Promotion.FindAsync(id);
            return data;
        }
        public async Task AddPromotion(Promotion p)
        {
            await db.Promotion.AddAsync(p);
            await db.SaveChangesAsync();
        }
        public async Task UpdatePromotion(Promotion p)
        {
            db.Promotion.Update(p);
            await db.SaveChangesAsync();
        }
        public async Task DeletePromotion(int id)
        {
            var data = await db.Promotion.FindAsync(id);
            if (data != null)
            {
                db.Promotion.Remove(data);
                await db.SaveChangesAsync();
            }
        }
        public async Task<List<User>> FetchUsers()
        {
            return await db.Users.ToListAsync();
        }
        public async Task<List<Designation>> FetchDesignations()
        {
            return await db.Designations.ToListAsync();
        }
    }
}


