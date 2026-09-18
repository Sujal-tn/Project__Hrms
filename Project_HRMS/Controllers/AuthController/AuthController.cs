using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface.LoginInterface;

namespace Project_Hrms.Controllers.AuthController
{
    public class AuthController : Controller
    {
        private readonly IAuthService auth;

        public AuthController(IAuthService auth)
        {
            this.auth = auth;
        }


        public IActionResult Index()
        {
            return View();
        }

         public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var us = auth.LoginUser(email, password);
            if (us != null)
            {
                HttpContext.Session.SetInt32("UserId", us.UserId);
                HttpContext.Session.SetString("UserName", us.FirstName + ' ' + us.LastName);
                HttpContext.Session.SetInt32("RoleId", us.RoleId);
                HttpContext.Session.SetString("RoleName", us.Role.RoleName);
                if (us.Role.RoleName == "Admin")
                {
                    return RedirectToAction("Auth", "AdminDashboard");
                }
                else if (us.Role.RoleName == "Manager")
                {

                    return RedirectToAction("Auth", "ManagerDashboard");

                }
                else if (us.Role.RoleName == "Employee")
                {

                    return RedirectToAction("Auth", "EmployeeDashboard");

                }
            }
            else
            {
                return RedirectToAction("Auth", "Login");
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View();

        }
    }
}
