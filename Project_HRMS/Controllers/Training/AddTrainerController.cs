using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Services.Training;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers.Training
{
    public class AddTrainerController : Controller
    {
        private readonly AddTrainerServicescs services;
        public AddTrainerController(AddTrainerServicescs services)
        {
            this.services = services;
        }

        public async Task<IActionResult> Index()
        {
            var data = await services.FetchAll();
            return View(data);
        }

        public IActionResult AddTrainer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainer(Trainers t)
        {
            if (ModelState.IsValid)
            {
                await services.AddTrainer(t);
                return RedirectToAction("Index");
            }
            return View(t);
        }

        public async Task<IActionResult> EditTrainer(int id)
        {
            var data = await services.FindByID(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditTrainer(Trainers t)
        {
            if (ModelState.IsValid)
            {
                await services.UpdateTrainer(t);
                return RedirectToAction("Index");
            }
            return View(t);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTrainer(int id)
        {
            var data = await services.FindByID(id);

            if (data == null)
            {
                return NotFound();
            }

            await services.DeleteTrainer(id);
            return RedirectToAction("Index");
        }

     

    }
}
