using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface.MasterDocuments;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers.MasterDocumentsController
{
    public class AdminAddDocumentsController : Controller
    {
        public readonly IAdminDocumentsServices services;
        public AdminAddDocumentsController(IAdminDocumentsServices services)
        {
            this.services = services;
        }
        public async Task<IActionResult> Index()
        {
            var data = await services.FetchAll();
            return View(data);
        }

        public IActionResult AddAdminDocuments()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddAdminDocuments(AdminAddDocumentsName t)
        {
            if (ModelState.IsValid)
            {
                await services.AddAdminDocuments(t);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditAdminDocuments(int id)
        {
            var data = await services.FindByID(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditAdminDocuments(AdminAddDocumentsName t)
        {
            if (ModelState.IsValid)
            {
                await services.UpdateAdminDocuments(t);
                return RedirectToAction("Index");
            }
            return View(t);
        }

       

        [HttpPost]
        public async Task<IActionResult> DeleteAdminDocuments(int id)
        {
            var data = await services.FindByID(id);

            if (data == null)
            {
                return NotFound();
            }
            await services.DeleteAdminDocuments(id);
            return RedirectToAction("Index");
        }


    }
}
