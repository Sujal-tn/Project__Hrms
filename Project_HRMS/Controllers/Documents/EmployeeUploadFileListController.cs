using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface.MasterDocuments.Documents;

namespace Project_Hrms.Controllers.Documents
{
    public class EmployeeUploadFileListController : Controller
    {
        private readonly IUploadedDocument services;
        private readonly IWebHostEnvironment env;

        public EmployeeUploadFileListController(IUploadedDocument services, IWebHostEnvironment env)
        {
            this.services = services;
            this.env = env; 
        }
        public async Task<IActionResult> Index()
        {
            var files = await services.FetchEmployeeFiles();
            return View(files);
        }


        public async Task<IActionResult> ViewFile(int id)
        {
            var file = await services.FindById(id);

            if (file == null)
            {
                return NotFound();
            }

            string filePath = Path.Combine(env.WebRootPath, "uploads", file.FileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            string content = await System.IO.File.ReadAllTextAsync(filePath);

            return Content(content, "text/plain");
        }

        public async Task<IActionResult> Download(int id)
        {
            var file = await services.FindById(id);

            if (file == null)
            {
                return NotFound();
            }

            string filePath = Path.Combine(env.WebRootPath, "uploads", file.FileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            return PhysicalFile(filePath, "application/octet-stream", file.FileName);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await services.DeleteFile(id);
            return RedirectToAction("Index");
        }
    }
}

