using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Controllers.EmployeeController
{
    public class DepartmentController : Controller
    {

        IDepartmentService ds;
        public DepartmentController(IDepartmentService ds)
        {
            this.ds = ds;
        }
        public IActionResult Index()
        {
          var deps =  ds.FetchDepartments();
            return View(deps);
        }

        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDepartment(Department d)
        {
            ds.AddDepartment(d);
            TempData["msg"] = "Department Added Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteDepartment(int id)
        {
            ds.DeleteDepartment(id);
            TempData["delmsg"] = "Department Deleted Successfully";
            return RedirectToAction("Index");

        }

        public IActionResult EditDepartment(int id)
        {
           var de= ds.findDepartmentById(id);
            return View(de);
        }

        [HttpPost]
        public IActionResult EditDepartment(Department de)
        {
            ds.UpdateDepartment(de);
            TempData["updmsg"] = "Department Updated Successfully";
            return RedirectToAction("Index");
        }
    }
}
