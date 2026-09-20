using Project_Hrms.Data;
using Project_Hrms.Models;
using Project_Hrms.Interface.PayrollInterface;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Services.PayrollService
{
    public class DeductionTypeService : IDeductionTypeService
    {
        private readonly ApplicationDbContext db;
        public DeductionTypeService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<DeductionType>> GetAllDeductionTypesAsync()
        {
            return await db.DeductionTypes.ToListAsync();
        }

        public async Task<DeductionType?> GetDeductionTypeByIdAsync(int id)
        {
            return await db.DeductionTypes.FindAsync(id);
        }

        public async Task AddDeductionTypeAsync(DeductionType deductionType)
        {
            db.DeductionTypes.Add(deductionType);
            await db.SaveChangesAsync();
        }

        public async Task UpdateDeductionTypeAsync(DeductionType deductionType)
        {
            db.DeductionTypes.Update(deductionType);
            await db.SaveChangesAsync();
        }

        public async Task<bool> DeleteDeductionTypeAsync(int id)
        {
            var entity = await db.DeductionTypes.FindAsync(id);
            if (entity == null) return false;

            db.DeductionTypes.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
