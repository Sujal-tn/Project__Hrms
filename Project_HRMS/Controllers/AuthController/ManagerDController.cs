using Microsoft.AspNetCore.Mvc;

namespace Project_Hrms.Controllers.AuthController
{
    public class ManagerDController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
