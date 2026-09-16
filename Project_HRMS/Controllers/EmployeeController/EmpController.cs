using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Controllers.EmployeeController
{
    public class EmpController : Controller
    {
        IEmpService es;
        public EmpController(IEmpService es)
        {
            this.es = es;
        }
        public IActionResult Index()
        {
            var allemps = es.FetchEmp();
            return View(allemps);
        }

        public IActionResult AddEmp()
        {
            var rs = es.fetchRole();
            ViewBag.Roles = rs;

            var ds = es.fetchDepartments();
            ViewBag.Departments = ds;

            var des = es.fetchDesignation();
            ViewBag.Designations = des;
            return View();
        }

        [HttpPost]
        public IActionResult AddEmp(User u)
        {
            es.AddEmp(u);
            TempData["msg"] = "Added Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteEmp(int id)
        {
            es.DeleteEmp(id);
            TempData["delmsg"] = "Added Successfully";
            return RedirectToAction("Index");

        }

        public IActionResult EditEmp()
        {
            return View();
        }

        public IActionResult EditEmp(User u)
        {
            es.UpdateEmp(u);
            TempData["updmsg"] = "Added Successfully";
            return RedirectToAction("Index");
        }

    }
}
