using Project_Hrms.Data;
using Project_Hrms.Models;
using Project_Hrms.Interface.PayrollInterface;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Services.PayrollService
{
    public class EarningTypeService : IEarningTypeService
    {
        private readonly ApplicationDbContext db;
        public EarningTypeService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<EarningType>> GetAllEarningTypesAsync()
        {
            return await db.EarningTypes.ToListAsync();
        }

        public async Task<EarningType?> GetEarningTypeByIdAsync(int id)
        {
            return await db.EarningTypes.FindAsync(id);
        }

        public async Task AddEarningTypeAsync(EarningType earningType)
        {
            db.EarningTypes.Add(earningType);
            await db.SaveChangesAsync();
        }

        public async Task UpdateEarningTypeAsync(EarningType earningType)
        {
            db.EarningTypes.Update(earningType);
            await db.SaveChangesAsync();
        }

        public async Task<bool> DeleteEarningTypeAsync(int id)
        {
            var entity = await db.EarningTypes.FindAsync(id);
            if (entity == null) return false;

            db.EarningTypes.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
