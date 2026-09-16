using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using Project_Hrms.Services;

namespace Project_Hrms.Controllers.Training
{
    public class TrainingTypeController : Controller
    {
        private readonly ITrainingType services;

        public TrainingTypeController(ITrainingType services) 
        { 
            this.services = services;
        }
       
        public async Task<IActionResult> Index()
        {
            var data = await services.FetchAll();
            return View(data);
        }

        public IActionResult AddType()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> AddType(TrainingType model)
        {
            if (ModelState.IsValid)
            {
                await services.AddTrainingType(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> EditType(int id)
        {
            var data = await services.FindByID(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditType(TrainingType model)
        {
            if (ModelState.IsValid)
            {
                await services.UpdateTrainingType(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteType(int id)
        {
            var data = await services.FindByID(id);

            if (data == null)
            {
                return NotFound();
            }

            await services.DeleteTrainingType(id);
            return RedirectToAction("Index");
        }

    
           
    }




}
