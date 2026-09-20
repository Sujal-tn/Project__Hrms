using Microsoft.AspNetCore.Mvc;

namespace Project_Hrms.Controllers.AuthController
{
    public class AdminDController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
