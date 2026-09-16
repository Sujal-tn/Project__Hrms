using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers
{
    public class PromotionController : Controller
    {
        IPromotion pro;
        public PromotionController(IPromotion pr)
        {
            this.pro = pr;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.Users = await pro.FetchUsers();
            ViewBag.Designations = await pro.FetchDesignations();
            var data = await pro.GetAllPromotions();
            return View(data);
        }
        public async Task<IActionResult> AddPromotion()
        {
            ViewBag.Users = await pro.FetchUsers();
            ViewBag.Designations = await pro.FetchDesignations();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddPromotion(Promotion p)
        {
            await pro.AddPromotion(p);
            TempData["addmssg"] = "Promotion added!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditPromotion(int id)
        {
            ViewBag.Users = await pro.FetchUsers();
            ViewBag.Designation = await pro.FetchDesignations();
            var data = await pro.GetAllPromotions();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditPromotion(Promotion p)
        {
            await pro.UpdatePromotion(p);
            TempData["addmssg"] = "Promotion updated!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeletePromotion(int id)
        {
            await pro.DeletePromotion(id);
            TempData["addmssg"] = "Promotion deleted!";
            return RedirectToAction("Index");
        }
    }
}
