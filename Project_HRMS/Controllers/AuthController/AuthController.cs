using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface.LoginInterface;
using Project_Hrms.Models.EmployeeModel;

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

                HttpContext.Session.SetString("UserId", us.UserId.ToString());
                HttpContext.Session.SetString("UserName", us.FirstName + ' ' + us.LastName);
                HttpContext.Session.SetInt32("RoleId", us.RoleId);
                if (us.Role.RoleName != null)
                {
                    HttpContext.Session.SetString("RoleName", us.Role.RoleName);
                }
                if (us.Role.RoleName == "Admin")
                {
                    return RedirectToAction("Index", "AdminD");
                }
                else if (us.Role.RoleName == "Manager")
                {

                    return RedirectToAction("Index", "ManagerD");

                }
                else if (us.Role.RoleName == "Employee")
                {

                    return RedirectToAction("Index", "EmployeeD");

                }
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View();

        }
    }
}
