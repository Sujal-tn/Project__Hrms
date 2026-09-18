using Project_Hrms.Data;
using Project_Hrms.Interface.MasterDocuments;
using Project_Hrms.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Services.MasterDocuments
{
    public class EmployeeDocumentsAddServices : IEmployeeDocumentsServices
    {
        private readonly ApplicationDbContext db;
        public EmployeeDocumentsAddServices(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddEmployeeDocuments(EmployeeAddDocumentsName t)
        {
            await db.EmployeeAddDocumentsNames.AddAsync(t);
            await db.SaveChangesAsync();
        }

        public async Task DeleteEmployeeDocuments(int id)
        {
            var data = await db.EmployeeAddDocumentsNames.FindAsync(id);
            if (data != null)
            {
                db.EmployeeAddDocumentsNames.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<EmployeeAddDocumentsName>> FetchAll()
        {
            var data = await db.EmployeeAddDocumentsNames.ToListAsync();
            return data;
        }

        public async Task<EmployeeAddDocumentsName?> FindByID(int id)
        {
            var data = await db.EmployeeAddDocumentsNames.FindAsync(id);
            return data;
        }
        

        public async Task UpdateEmployeeDocuments(EmployeeAddDocumentsName t)
        {
            db.EmployeeAddDocumentsNames.Update(t);
            await db.SaveChangesAsync();
        }
    }
}
