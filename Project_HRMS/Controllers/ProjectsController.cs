using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers
{
    public class ProjectsController : Controller
    {
        IProject pro;
        private readonly IWebHostEnvironment environment;
        public ProjectsController(IProject pr, IWebHostEnvironment env)
        {
            this.pro = pr;
            this.environment = env;
        }
        public async Task<IActionResult> Index()
        {
            var data = await pro.GetAllProjects();
            return View(data);
        }

        public IActionResult AddProject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(ProjectsView p)
        {

            Projects project = new Projects()
            {
                ProjectName = p.ProjectName,
                ClientName = p.ClientName,
                ProjectDescription = p.ProjectDescription,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Priority = p.Priority,
                ProjectValue = p.ProjectValue,
                PriceType = p.PriceType,
                Status = p.Status,
                ManagerName = p.ManagerName
            };

            string path = environment.WebRootPath + "/Content/Logo";
            string fileName = p.LogoPath.FileName;
            string fullPath = Path.Combine(path, fileName);
            FileStream stream = new FileStream(fullPath, FileMode.Create);
            p.LogoPath.CopyTo(stream);
            stream.Close();
            project.LogoPath = "/Content/Logo/" + fileName;
            string fileName2 = p.FilePath.FileName;
            string path2 = environment.WebRootPath + "/Content/Files";
            string fullPath2 = Path.Combine(path2, fileName2);
            FileStream stream2 = new FileStream(fullPath2, FileMode.Create);
            p.FilePath.CopyTo(stream2);
            stream2.Close();
            project.FilePath = "/Content/Files/" + fileName2;

            await pro.AddNewProject(project);
            TempData["addmsg"] = "Project Added Successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditProject(int id)
        {
            var data = await pro.FindProjectById(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditProject(Projects p)
        {
            await pro.UpdateProject(p);
            TempData["updmsg"] = "Project Updated Successfully";
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> DeleteProject(int id)
        {
            await pro.DeleteProject(id);
            TempData["delmsg"] = "Project Deleted Successfully";
            return RedirectToAction("Index");
        }
    } 
}
