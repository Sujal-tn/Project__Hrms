using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers
{
    public class ResignationController : Controller
    {
        IResignation res;

        public ResignationController(IResignation re)
        {
            this.res = re;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Users = await res.FetchUsers();
            ViewBag.Departments = await res.FetchDepartments();
            var data = await res.GetAllResignations();
            return View(data);
        }


        public async Task<IActionResult> AddResignation()
        {
            ViewBag.Users = await res.FetchUsers();
            ViewBag.Departments = await res.FetchDepartments();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddResignation(Resignation r)
        {
            await res.AddResignation(r);
            TempData["addmssg"] = "Resignation added!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditResignation(int id)
        {
            ViewBag.Users = await res.FetchUsers();
            ViewBag.Departments = await res.FetchDepartments();
            var data = await res.FindResignationById(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditResignation(Resignation r)
        {
            await res.UpdateResignation(r);
            TempData["addmssg"] = "Resignation updated!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteResignation(int id)
        {
            await res.DeleteResignation(id);
            TempData["addmssg"] = "Resignation deleted!";
            return RedirectToAction("Index");
        }
    }
}
