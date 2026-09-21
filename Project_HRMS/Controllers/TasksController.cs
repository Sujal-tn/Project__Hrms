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
        IUserService userService;
        private readonly IWebHostEnvironment environment;

        public TasksController(ITask t,IProject P,IUserService us,IWebHostEnvironment e)
        {
            this.task = t;
            this.pro = P;
            this.userService = us;
            this.environment = e;
        }

        public async Task<IActionResult> Index(string priority, DateTime? dueDate)
        {
            var data = await task.GetAllTasks();
            if (!string.IsNullOrEmpty(priority) && priority != "All")
            {
                data = data.Where(x => x.Priority == priority).ToList();
            }

            if (dueDate.HasValue)
            {
                data = data.Where(x => x.Deadline.Date == dueDate.Value.Date).ToList();
            }
            return View(data);
        }

        public async Task<IActionResult> AddTask()
        {
            ViewBag.Projects = await pro.GetAllProjects();
            ViewBag.Users = await userService.FeatchUser();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(Tasks t,IFormFile file,int? UserId)
        {
            if (file != null)
            {
                string folder = Path.Combine(environment.WebRootPath,"Content","Files");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName =Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath =Path.Combine(folder, fileName);

                using (var stream =new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                t.FilePath = "/Content/Files/" + fileName;
            }

            int taskId = await task.AddNewTask(t);

            if (UserId.HasValue)
            {
                TaskMembers member = new TaskMembers();

                member.TaskId = taskId;
                member.UserId = UserId.Value;
                await task.AddTaskMember(member);
            }

            TempData["SucMsg"] = "Task added successfully!";
            return RedirectToAction("Index");
        }
    }
}