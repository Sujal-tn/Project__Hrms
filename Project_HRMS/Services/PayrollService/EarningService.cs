using Project_Hrms.Data;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;
using Project_Hrms.Interface.PayrollInterface;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Services.PayrollService
{
    public class EarningService : IEarningService
    {
        private readonly ApplicationDbContext db;
        public EarningService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Earning>> GetAllEarningsAsync()
        {
            return await db.Earnings
                .Include(e => e.EarningType)
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .ToListAsync();
        }

        public async Task<Earning?> GetEarningByIdAsync(int id)
        {
            return await db.Earnings
                .Include(e => e.EarningType)
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .FirstOrDefaultAsync(e => e.EarningsId == id);
        }

        public async Task AddEarningAsync(Earning earning)
        {
            earning.CreatedAt = DateTime.UtcNow;
            db.Earnings.Add(earning);
            await db.SaveChangesAsync();
        }

        public async Task UpdateEarningAsync(Earning earning)
        {
            earning.ModifiedAt = DateTime.UtcNow;
            db.Earnings.Update(earning);
            await db.SaveChangesAsync();
        }

        public async Task<bool> DeleteEarningAsync(int id)
        {
            var entity = await db.Earnings.FindAsync(id);
            if (entity == null) return false;

            db.Earnings.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Earning>> GetEarningsByDeptDesignationAsync(int departmentId, int designationId)
        {
            return await db.Earnings
                .Include(e => e.EarningType)
                .Where(e => e.DepartmentId == departmentId && e.DesignationId == designationId)
                .ToListAsync();
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await db.Departments.ToListAsync();
        }

        public async Task<List<Designation>> GetDesignationsAsync()
        {
            return await db.Designations.ToListAsync();
        }
    }
}
