using Project_Hrms.Data;
using Project_Hrms.Interface.MasterDocuments.Documents;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Services.Documents
{
    public class AdminFileUploadServices : IAdminFileUpload
    {
            private readonly ApplicationDbContext db;
            private readonly IWebHostEnvironment env;

            public AdminFileUploadServices( ApplicationDbContext db,IWebHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
        
            public async Task<List<User>> FetchAllUsers()
            {
                var data = await db.Users.ToListAsync();

                return data;
            }
            public async Task<List<AdminAddDocumentsName>> FetchAllDocumentNames()
            {
                var data = await db.AdminAddDocumentsNames.ToListAsync();

                return data;
            }

            public async Task SaveFiles(AdminFileUploadViewModel model)
            {
                string uploadFolder = Path.Combine( env.WebRootPath, "uploads" );

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                foreach (var document in model.Documents)
                {
                    if (document.File != null)
                    {
                        string filePath = Path.Combine( uploadFolder, document.File.FileName);
                        using var stream = new FileStream(filePath, FileMode.Create);
                         await document.File.CopyToAsync(stream);
                    }
                }
            }


    }

    
}
