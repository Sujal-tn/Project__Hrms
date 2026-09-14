using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers
{
    public class ProjectsController : Controller
    {
        IProject pro;
        public ProjectsController(IProject pr)
        {
            this.pro = pr;
        }
        public IActionResult Index()
        {
            var data = pro.GetAllProjects();
            return View(data);
        }

        public IActionResult AddProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProject(Projects p)
        {
            pro.AddNewProject(p);
            TempData["addmsg"] = "Project Added Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult EditProject(int id)
        {
            var data = pro.FindProjectById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditProject(Projects p)
        { 
            pro.UpdateProject(p);
            TempData["updmsg"] = "Project Updated Successfully";
            return RedirectToAction("Index");

        }

        public IActionResult DeleteProject(int id)
        {
            pro.DeleteProject(id);
            TempData["delmsg"] = "Project Deleted Successfully";
            return RedirectToAction("Index");
        }
    } 
}
