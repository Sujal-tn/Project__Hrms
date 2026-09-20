using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface.MasterDocuments.Documents;
using Project_Hrms.Models;

namespace Project_Hrms.Services.Documents
{
    public class UploadedDocumentListServices : IUploadedDocument
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment env;

        public UploadedDocumentListServices(ApplicationDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

       

        public async Task<FileUploads?> FindById(int id)
        {
            var data = await db.Files
                .Include(x => x.Document)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.id == id);
            return data;
        }

        public async Task DeleteFile(int id)
        {
            var file = await db.Files.FindAsync(id);

            if (file != null)
            {
                string filePath = Path.Combine(env.WebRootPath, "uploads", file.FileName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                db.Files.Remove(file);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<FileUploads>> FetchAdminFiles()
        {
            var data = await db.Files
                .Include(x => x.Document)
                .Include(x => x.User)
                .Where(x => x.Document.DocumentType == "Admin")
                .ToListAsync();

            return data;
        }

        public async Task<List<FileUploads>> FetchEmployeeFiles()
        {
            var data = await db.Files
                 .Include(x => x.Document)
                 .Include(x => x.User)
                 .Where(x => x.Document.DocumentType == "Employee")
                 .ToListAsync();

            return data;
        }
    }
}
