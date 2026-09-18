using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using Microsoft.AspNetCore.Hosting;

namespace Project_Hrms.Controllers
{
    public class TasksController : Controller
    {
        ITask task;
        IProject pro;
        private readonly IWebHostEnvironment environment;
        public TasksController(ITask t,IProject P, IWebHostEnvironment e)
        {
            this.task = t;
            this.pro = P;
            this.environment = e;
        }
        public async Task <IActionResult>Index()
        {
            var data = await task.GetAllTasks();
            return View(data);
        }

        public async Task<IActionResult> AddTask()
        {
            ViewBag.Projects = await pro.GetAllProjects();

            return View();
        }


        [HttpPost]
        public async Task <IActionResult> AddTask(Tasks t, IFormFile file)
        {
            if (file != null)
            {
                string folder = Path.Combine(environment.WebRootPath, "Content", "Files");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(folder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                t.FilePath = "/Content/Files/" + fileName;
            }

            await task.AddNewTask(t);
            TempData["SucMsg"] = "Task added successfully!";
            return RedirectToAction("Index");
        }

        //public async Task <IActionResult> EditTask(int id)
        //{
        //    var data = await task.FindTaskById(id);
        //    ViewBag.Projects = await pro.GetAllProjects();
        //    return View(data);
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditTask(Tasks t)
        //{
        //    await task.UpdateTask(t);
        //    TempData["UpdMsg"] = "Task updated successfully!";
        //    return RedirectToAction("Index");

        //}

        //public async Task<IActionResult> DeleteTask(int id)
        //{
        //    await task.DeleteTask(id);
        //    TempData["DelMsg"] = "Task deleted successfully!";
        //    return RedirectToAction("Index");
        //}
    }
}
