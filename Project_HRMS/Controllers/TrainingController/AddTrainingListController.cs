using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface.TrainingInterface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers.Training
{
    public class AddTrainingListController : Controller
    {
        private readonly ITrainingList services;

        public AddTrainingListController(ITrainingList services)
        {
            this.services = services;
        }


        public async Task<IActionResult> Index()
        {
            var data = await services.FetchAll();

            ViewBag.Trainers = await services.FetchAllTrainers();
            ViewBag.TrainingTypes = await services.FetchAllTrainingTypes();
            ViewBag.Employees = await services.FetchAllUsers();

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> AddTrainingList()
        {
            ViewBag.Trainers = await services.FetchAllTrainers();
            ViewBag.TrainingTypes = await services.FetchAllTrainingTypes();
            ViewBag.Employees = await services.FetchAllUsers();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> AddTrainingList(Trainings t)
        {
            t.CreatedAt = DateTime.Now;
            t.CreatedBy = "Admin";
            t.ModifiedAt = DateTime.Now;
            t.ModifiedBy = "Admin";

            if (ModelState.IsValid)
            {
                await services.AddTrainingList(t);

                return RedirectToAction("Index");
            }

            ViewBag.Trainers = await services.FetchAllTrainers();
            ViewBag.TrainingTypes = await services.FetchAllTrainingTypes();
            ViewBag.Employees = await services.FetchAllUsers();

            return RedirectToAction("Index");

        }


        [HttpGet]
        public async Task<IActionResult> EditTrainingList(int id)
        {
            var data = await services.FindByID(id);

            if (data == null)
            {
                return NotFound();
            }

            ViewBag.Trainers = await services.FetchAllTrainers();
            ViewBag.TrainingTypes = await services.FetchAllTrainingTypes();
            ViewBag.Employees = await services.FetchAllUsers();

            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> EditTrainingList(Trainings t)
        {
            var data = await services.FindByID(t.TrainingId);

            if (data== null)
            {
                return NotFound();
            }

            data.TrainerId = t.TrainerId;
            data.TrainingTypeId = t.TrainingTypeId;
            data.UserId = t.UserId;
            data.TrainingCost = t.TrainingCost;
            data.Description = t.Description;
            data.Status = t.Status;
            data.StartDate = t.StartDate;
            data.EndDate = t.EndDate;

            data.ModifiedAt = DateTime.Now;
            data.ModifiedBy = "Admin";

            await services.UpdateTrainingList(data);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteTrainingList(int id)
        {
            var data = await services.FindByID(id);

            if (data == null)
            {
                return NotFound();
            }

            await services.DeleteTrainingList(id);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> FetchAllTrainers()
        {
            var data = await services.FetchAllTrainers();

            return View(data);
        }


        public async Task<IActionResult> FetchAllTrainingTypes()
        {
            var data = await services.FetchAllTrainingTypes();

            return View(data);
        }


        public async Task<IActionResult> FetchAllUsers()
        {
            var data = await services.FetchAllUsers();

            return View(data);
        }
    }
}