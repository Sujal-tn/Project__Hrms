using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface.MasterDocuments;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers.MasterDocumentsController
{
    public class EmployeeAddDocumentsController : Controller
    {
        public readonly IEmployeeDocumentsServices services;

        public EmployeeAddDocumentsController(IEmployeeDocumentsServices services)
        {
            this.services = services;
        }   
        public async Task<IActionResult> Index()
        {
           var data = await services.FetchAll();
            return View(data);
        }

        public async Task<IActionResult> AddEmployeeDocuments()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeDocuments(EmployeeAddDocumentsName t)
        {
            if (ModelState.IsValid)
            {
                await services.AddEmployeeDocuments(t);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditEmployeeDocuments(int id)
        {
            var data = await services.FindByID(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployeeDocuments(EmployeeAddDocumentsName t)
        {
            if (ModelState.IsValid)
            {
                await services.UpdateEmployeeDocuments(t);
                return RedirectToAction("Index");
            }
            return View(t);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployeeDocuments(int id)
        {
            await services.DeleteEmployeeDocuments(id);
            return RedirectToAction("Index");
        }


    }
}
