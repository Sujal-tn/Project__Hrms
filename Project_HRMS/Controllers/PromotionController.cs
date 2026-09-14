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
            return View();
        }
    }
}
