using DocumentFormat.OpenXml.Office2010.Excel;
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

        public async Task<IActionResult> AddEmp(int? roleId)
        {
            var rs = es.fetchRole();
            ViewBag.Role = rs;

            var ds = es.fetchDepartments();
            ViewBag.Depart = ds;

            var des = es.fetchDesignation();
            ViewBag.Desig = des;

            var ma = await es.FetchManagersAsync();
            ViewBag.Man = ma;


            if (roleId != null)
            {
                ViewBag.rid = roleId.Value;

                ViewBag.rname= es.GetRoleName(roleId.Value);
            }
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

        public async Task<IActionResult> EditEmp(int id)
        {
            var emp = es.findEmpById(id);

            if (emp == null)
            {
                return NotFound();
            }

            var rs = es.fetchRole();
            ViewBag.Role = rs;

            var ds = es.fetchDepartments();
            ViewBag.Depart = ds;

            var des = es.fetchDesignation();
            ViewBag.Desig = des;

            var ma = await es.FetchManagersAsync();
            ViewBag.Man = ma;
            return View(emp);
        }
        [HttpPost]
        public IActionResult EditEmp(User u)
        {
            es.UpdateEmp(u);
            TempData["updmsg"] = "Added Successfully";
            return RedirectToAction("Index");
        }

    }
}
