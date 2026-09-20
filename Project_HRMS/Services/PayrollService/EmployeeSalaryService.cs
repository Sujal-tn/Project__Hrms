using Project_Hrms.Data;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;
using Project_Hrms.Interface.PayrollInterface;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Services.PayrollService
{
    public class EmployeeSalaryService : IEmployeeSalaryService
    {
        private readonly ApplicationDbContext db;
        public EmployeeSalaryService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<EmployeeSalaries>> GetAllEmployeeSalariesAsync(string? designation, string? dateRange, string? sortBy)
        {
            var query = db.EmployeeSalaries
                .Include(e => e.User)
                    .ThenInclude(u => u.Designation)
                .Include(e => e.User)
                    .ThenInclude(u => u.Role)
                .AsQueryable();

            query = query.Where(e => e.User.Role.RoleName != "Admin");

            if (!string.IsNullOrEmpty(designation))
                query = query.Where(e => e.User.Designation.DesignationName == designation);

            if (!string.IsNullOrEmpty(dateRange))
            {
                var dates = dateRange.Split(" - ");
                if (dates.Length == 2
                    && DateTime.TryParse(dates[0], out var startDate)
                    && DateTime.TryParse(dates[1], out var endDate))
                {
                    query = query.Where(e => e.CreatedDate >= startDate && e.CreatedDate <= endDate);
                }
            }

            query = sortBy switch
            {
                "Ascending" => query.OrderBy(e => e.User.FirstName),
                "Descending" => query.OrderByDescending(e => e.User.FirstName),
                "Last Month" => query.Where(e => e.CreatedDate >= DateTime.Now.AddMonths(-1)),
                "Last 7 Days" => query.Where(e => e.CreatedDate >= DateTime.Now.AddDays(-7)),
                _ => query.OrderByDescending(e => e.CreatedDate)
            };

            return await query.ToListAsync();
        }

        public async Task<EmployeeSalaries?> GetEmployeeSalaryByIdAsync(int id)
        {
            return await db.EmployeeSalaries
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.SalaryId == id);
        }

        public async Task<EmployeeSalaries?> GetLatestSalaryByUserIdAsync(int userId)
        {
            return await db.EmployeeSalaries
                .Include(e => e.EmployeeEarnings)
                    .ThenInclude(ee => ee.Earning)
                        .ThenInclude(er => er.EarningType)
                .Include(e => e.EmployeeDeductions)
                    .ThenInclude(ed => ed.Deduction)
                        .ThenInclude(d => d.DeductionType)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.CreatedDate)
                .FirstOrDefaultAsync();
        }

        public async Task<(List<Earning> Earnings, List<Deduction> Deductions)> GetApplicableRulesForUserAsync(int userId)
        {
            var user = await db.Users.FindAsync(userId);
            if (user == null) return (new List<Earning>(), new List<Deduction>());

            var earnings = await db.Earnings
                .Include(e => e.EarningType)
                .Where(e => e.DepartmentId == user.DepartmentId && e.DesignationId == user.DesignationtId)
                .ToListAsync();

            var deductions = await db.Deductions
                .Include(d => d.DeductionType)
                .Where(d => d.DepartmentId == user.DepartmentId && d.DesignationId == user.DesignationtId)
                .ToListAsync();

            return (earnings, deductions);
        }

        public async Task<(List<Earning> Earnings, List<Deduction> Deductions)> GetAllActiveRulesAsync()
        {
            var earnings = await db.Earnings.Include(e => e.EarningType).ToListAsync();
            var deductions = await db.Deductions.Include(d => d.DeductionType).ToListAsync();
            return (earnings, deductions);
        }

        public async Task AddEmployeeSalaryAsync(EmployeeSalaries salary, List<EmployeeEarnings> earnings, List<EmployeeDeductions> deductions)
        {
            decimal totalEarnings = earnings.Sum(e => e.EarningAmount);
            decimal totalDeductions = deductions.Sum(d => d.DeductionAmount);

            salary.NetSalary = salary.TotalSalary + totalEarnings - totalDeductions;
            salary.CreatedDate = DateTime.Now;

            db.EmployeeSalaries.Add(salary);
            await db.SaveChangesAsync();

            foreach (var earning in earnings)
            {
                earning.SalaryId = salary.SalaryId;
                earning.UserId = salary.UserId;
                db.EmployeeEarnings.Add(earning);
            }

            foreach (var deduction in deductions)
            {
                deduction.SalaryId = salary.SalaryId;
                deduction.UserId = salary.UserId;
                db.EmployeeDeductions.Add(deduction);
            }

            await db.SaveChangesAsync();
        }

        public async Task<bool> DeleteEmployeeSalaryAsync(int id)
        {
            var salary = await db.EmployeeSalaries.FindAsync(id);
            if (salary == null) return false;

            db.EmployeeSalaries.Remove(salary);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
