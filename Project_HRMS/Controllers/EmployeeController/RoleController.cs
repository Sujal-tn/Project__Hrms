using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Controllers.EmployeeController
{
    public class RoleController : Controller
    {
        IRoleService rs;
        public RoleController(IRoleService rs)
        {
            this.rs = rs;
        }
        public IActionResult Index()
        {
            var allroles = rs.FetchRoles();
            return View(allroles);
        }

        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddRole(Role r)
        {
            rs.AddRole(r);
            TempData["msg"] = "Role Added Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteRole(int id)
        {
            rs.DeleteRole(id);
            TempData["DelMsg"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult EditRole(int id)
        {
            var ed = rs.findRoleById(id);
            return View(ed);
        }
        [HttpPost]
        public IActionResult EditRole(Role r)
        {
            rs.UpdateRole(r);
            TempData["UpdMsg"] = "Updated Successfully";
            return RedirectToAction("Index");
        }
    }
}
