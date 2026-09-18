using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface.MasterDocuments.Documents;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers.Documents
{
    public class AdminFilesUploadController : Controller
    {
        private readonly IAdminFileUpload services;
        public AdminFilesUploadController(IAdminFileUpload services)
        {
            this.services = services;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Users = await services.FetchAllUsers();
            ViewBag.DocumentNames =await services.FetchAllDocumentNames();
            var model = new AdminFileUploadViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AdminFileUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                await services.SaveFiles(model);
                TempData["SuccessMessage"] ="Files uploaded successfully!";
                return RedirectToAction("Index");
            }

            ViewBag.Users = await services.FetchAllUsers();
            ViewBag.DocumentNames =await services.FetchAllDocumentNames();
            return View(model);
        }


    }
}
