using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using Microsoft.EntityFrameworkCore;

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
            var data = await db.Payslips.ToListAsync();

            return data;
        }
    }
}