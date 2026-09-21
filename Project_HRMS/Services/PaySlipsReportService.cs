using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Service
{
    public class PaySlipsReportService : IPaySlipsReportService
    {
        private readonly ApplicationDbContext db;

        public PaySlipsReportService(ApplicationDbContext db)
        {
            this.db = db;
        }


        public async Task<List<Payslips>> FeatchPaySlips()
        {
            
            var data = await db.Payslips
                .Include(x => x.User)
                .ToListAsync();

            foreach (var item in data)
            {
                item.NetPay =
                    item.TotalSalary
                    - item.Deductions
                    + item.Earnings;
            }

            return data;
        }


        public async Task<List<Payslips>> GetSalaryGraphData()
        {
            var data = await db.Payslips
                .GroupBy(x => new
                {
                    x.Year,
                    x.Month
                })
                .Select(x => new Payslips
                {
                    Year = x.Key.Year,
                    Month = x.Key.Month,

                    TotalSalary = x.Sum(a => a.TotalSalary),

                    Deductions = x.Sum(a => a.Deductions),

                    Earnings = x.Sum(a => a.Earnings)
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            foreach (var item in data)
            {
                item.NetPay =
                    item.TotalSalary
                    - item.Deductions
                    + item.Earnings;
            }

            return data;
        }


        public async Task<byte[]> ExportToPDF()
        {
            return Array.Empty<byte>();
        }


        public async Task<byte[]> ExportToExcel()
        {
            return Array.Empty<byte>();
        }
    }
}
