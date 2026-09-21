using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface.MasterDocuments.Documents;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers.Documents
{
    public class EmployeeFilesUploadController : Controller
    {
        private readonly IEmployeeFileUpload services;
        
        public EmployeeFilesUploadController(IEmployeeFileUpload services)
        {
            this.services = services;
        }
        public async Task<IActionResult> Index() 
        {
            ViewBag.Users = await services.FetchAllUser();
            ViewBag.DocumentNames = await services.FetchAllDocuments();
            var model = new EmployeeFileUploadViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(EmployeeFileUploadViewModel model)
        {
            if(ModelState.IsValid)
            {
                await services.SaveFiles(model);
                TempData["SuccessMessage"] = "Files uploaded on Email also successfully !";
                return RedirectToAction("Index");
            }

            ViewBag.Users = await services.FetchAllUser();
            ViewBag.DocumentNames = await services.FetchAllDocuments();
            return View(model);

        }
    }
}
