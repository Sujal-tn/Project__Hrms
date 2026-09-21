using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Project_Hrms.Interface.MasterDocuments.Documents;

namespace Project_Hrms.Controllers.Documents
{
    public class AdminUploadFileListController : Controller
    {
        private readonly IUploadedDocument services;
         private readonly IWebHostEnvironment env;

        public AdminUploadFileListController(IUploadedDocument services,IWebHostEnvironment env)
        {
            this.services = services;
            this.env = env;
        }
        public async Task<IActionResult> Index()
        {
            var files = await services.FetchAdminFiles();
            return View(files);
        }

     
        public async Task<IActionResult> ViewFile(int id)
        {
            var file = await services.FindById(id);

            if (file == null)
            {
                return NotFound();
            }

            string filePath = Path.Combine(env.WebRootPath, "uploads", file.FileName );

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            //var provider = new FileExtensionContentTypeProvider();

            //if (!provider.TryGetContentType(file.FileName, out string? contentType))
            //{
            //    contentType = "application/octet-stream";
            //}

            //Response.Headers.Append(
            //    "Content-Disposition",
            //    "inline; filename=\"" + file.FileName + "\"");

            //return PhysicalFile(
            //    filePath,
            //    contentType);

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

            string filePath = Path.Combine(env.WebRootPath, "uploads",file.FileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            return PhysicalFile(filePath,"application/octet-stream", file.FileName );
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await services.DeleteFile(id);
            return RedirectToAction("Index");
        }
    }
}
