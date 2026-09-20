using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers
{
    public class MasterEventsController : Controller
    {
        IMasterEvent me;
        public MasterEventsController(IMasterEvent E)
        {
            this.me = E;
        }

        public async Task<IActionResult> Index()
        {
            var data = await me.GetAllMasterEvents();
            return View(data);
        }

        public IActionResult AddMasterEvent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMasterEvent(MasterEvents e)
        {
            await me.AddNewMasterEvent(e);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMasterEvent(int id)
        {
            await me.DeleteMasterEvent(id);
            return RedirectToAction("Index");
        }
    }
}