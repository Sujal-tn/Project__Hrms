using Project_Hrms.Data;
using Project_Hrms.Interface.MasterDocuments;
using Project_Hrms.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Services.MasterDocuments
{
    public class AdminDocumentsAddServices : IAdminDocumentsServices
    {
        private readonly ApplicationDbContext db;
        public AdminDocumentsAddServices(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddAdminDocuments(AdminAddDocumentsName t)
        {
            await db.AdminAddDocumentsNames.AddAsync(t);

            var uploadDoc = new UploadDocuments
            {
                DocumentName = t.ADocumentName,
                DocumentType = "Admin"
            };
            
            await db.MasterDocument.AddAsync(uploadDoc);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAdminDocuments(int id)
        {
            var data = await db.AdminAddDocumentsNames.FindAsync(id);

            if (data != null)
            {
                db.AdminAddDocumentsNames.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<AdminAddDocumentsName>> FetchAll()
        {
            var data = await db.AdminAddDocumentsNames.ToListAsync();
            return data;
        }

         
        public async Task<AdminAddDocumentsName?> FindByID(int id)
        {
            var data = await db.AdminAddDocumentsNames.FindAsync(id);
            return data;
        }

        public async Task UpdateAdminDocuments(AdminAddDocumentsName t)
        {
            db.AdminAddDocumentsNames.Update(t);
            await db.SaveChangesAsync();
        }
    }
}
