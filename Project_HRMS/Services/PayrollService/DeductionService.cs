using Project_Hrms.Data;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;
using Project_Hrms.Interface.PayrollInterface;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Services.PayrollService
{
    public class DeductionService : IDeductionService
    {
        private readonly ApplicationDbContext db;
        public DeductionService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Deduction>> GetAllDeductionsAsync()
        {
            return await db.Deductions
                .Include(d => d.DeductionType)
                .Include(d => d.Department)
                .Include(d => d.Designation)
                .ToListAsync();
        }

        public async Task<Deduction?> GetDeductionByIdAsync(int id)
        {
            return await db.Deductions
                .Include(d => d.DeductionType)
                .Include(d => d.Department)
                .Include(d => d.Designation)
                .FirstOrDefaultAsync(d => d.DeductionId == id);
        }

        public async Task AddDeductionAsync(Deduction deduction)
        {
            deduction.CreatedAt = DateTime.UtcNow;
            db.Deductions.Add(deduction);
            await db.SaveChangesAsync();
        }

        public async Task UpdateDeductionAsync(Deduction deduction)
        {
            deduction.ModifiedAt = DateTime.UtcNow;
            db.Deductions.Update(deduction);
            await db.SaveChangesAsync();
        }

        public async Task<bool> DeleteDeductionAsync(int id)
        {
            var entity = await db.Deductions.FindAsync(id);
            if (entity == null) return false;

            db.Deductions.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<Deduction>> GetDeductionsByDeptDesignationAsync(int departmentId, int designationId)
        {
            return await db.Deductions
                .Include(d => d.DeductionType)
                .Where(d => d.DepartmentId == departmentId && d.DesignationId == designationId)
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
