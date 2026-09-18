using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers
{
    public class EventsController : Controller
    {
        IEvent Ev;
        IMasterEvent me;

        public EventsController(IEvent E, IMasterEvent M)
        {
            this.Ev = E;
            this.me = M;
        }

        public async Task<IActionResult> Index()
        {
            var data = await Ev.GetAllEvents();
            ViewBag.MasterEvents = await me.GetAllMasterEvents();
            return View(data);
        }

        public async Task<IActionResult> AddEvent()
        {
            var data = await Ev.GetAllEvents();
            ViewBag.MasterEvents = await me.GetAllMasterEvents();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(Events e)
        {
            await Ev.AddNewEvent(e);
            return RedirectToAction("AddEvent");
        }

        public async Task<IActionResult> EditEvent(int id)
        {
            var data = await Ev.FindEventById(id);
            ViewBag.MasterEvents = await me.GetAllMasterEvents();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(Events e)
        {
            await Ev.UpdateEvent(e);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await Ev.DeleteEvent(id);
            return RedirectToAction("Index");
        }
    }
}