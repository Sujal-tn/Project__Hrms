using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface.MasterDocuments;
using Project_Hrms.Interface.MasterDocuments.Documents;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Services.Documents
{
    public class EmployeeFileUploadServices : IEmployeeFileUpload
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment env;
        private readonly IEmailService emailService;
        public EmployeeFileUploadServices(ApplicationDbContext db ,IWebHostEnvironment env , IEmailService emailService)
        {
            this.db = db;
            this.env = env;
            this.emailService = emailService;

        }

        public async Task<List<UploadDocuments>> FetchAllDocuments()
        {
            var data = await db.MasterDocument
                   .Where(x => x.DocumentType == "Employee")
                   .ToListAsync();
            return data;
        }

        public async Task<List<User>> FetchAllUser()
        {
            var data = await db.Users.ToListAsync();
            return data;
        }


        public async Task SaveFiles(EmployeeFileUploadViewModel model)
        {
            string uploadfolder = Path.Combine(env.WebRootPath, "uploads");

            if (!Directory.Exists(uploadfolder))
            {
                Directory.CreateDirectory(uploadfolder);
            }

            List<string> filePaths = new List<string>();

            foreach (var documents in model.Documents)
            {
                if (documents.File != null)
                {
                    string orgfilename = documents.File.FileName;

                    string filename = Guid.NewGuid().ToString()
                                      + Path.GetExtension(orgfilename);

                    string filepath = Path.Combine(uploadfolder, filename);
                    using (FileStream stream = new FileStream(filepath, FileMode.Create))
                    {
                        await documents.File.CopyToAsync(stream);
                    }

                    var fileData = new FileUploads
                    {
                        FileName = filename,
                        FilePath = "/uploads/" + filename,
                        UserId = model.UserId,
                        DocumentId = documents.DocumentId
                    };

                    db.Files.Add(fileData);

                    filePaths.Add(filepath);
                }
            }
            await db.SaveChangesAsync();

            var user = await db.Users
                .FirstOrDefaultAsync(x => x.UserId == model.UserId);

            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                await emailService.SendEmailAsync(
                    user.Email,
                    "Documents Uploaded",
                    "Your documents have been uploaded successfully.",
                    filePaths);
            
            }
       
        }


    }
}
