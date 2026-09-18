using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using Project_Hrms.Models;


namespace Project_Hrms.Controllers
{
    public class TerminationController : Controller
    {
        ITermination ter;
        public TerminationController(ITermination ter)
        {
            this.ter = ter;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Users = await ter.FetchUsers();
            var data = await ter.GetAllTerminations();
            return View(data);
        }
        public async Task<IActionResult> AddTermination()
        {
            ViewBag.Users = await ter.FetchUsers();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTermination(Termination t)
        {
            await ter.AddTermination(t);
            TempData["addmssg"] = "Termination added";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditTermination()
        {
            ViewBag.Users = await ter.FetchUsers();
            var data = await ter.GetAllTerminations();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditTermination(Termination t)
        {
            await ter.UpdateTermination(t);
            TempData["addmssg"] = "Termination updated";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteTermination(int id)
        {
            await ter.DeleteTermination(id);
            TempData["addmssg"] = "Termiantion deleted";
            return RedirectToAction("Index");
        }
    }
}
