using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Controllers.EmployeeController
{
    public class DesignationController : Controller
    {
        private readonly IDesignationService ds;
        public DesignationController(IDesignationService ds)
        {
            this.ds = ds;
        }
        public IActionResult Index()
        {
            var de = ds.FetchDesignation();
            var dept = ds.fetchDepartments();
            ViewBag.Departments = dept;
            return View(de);
        }

        public IActionResult AddDesignation()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddDesignation(Designation d)
        {
            ds.AddDesignation(d);
            TempData["msg"] = "Designation Added Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteDesignation(int id)
        {
            ds.DeleteDesignation(id);
            TempData["delmsg"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult EditDesignation(int id)
        {
            var dept = ds.fetchDepartments();
            ViewBag.Departments = dept;
            var d = ds.findDesignationById(id);
            return View(d);
        }

        [HttpPost]
        public IActionResult EditDesignation(Designation d)
        {
            ds.UpdateDesignation(d);
            TempData["UpdMsg"] = "Updated Successfully";
            return RedirectToAction("Index");
        }

    }
}
