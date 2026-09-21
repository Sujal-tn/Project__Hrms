using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers
{
    public class TaskBoardsController : Controller
    {
        ITaskBoard taskBoard;
        IProject project;
        ITask task;

        public TaskBoardsController(ITaskBoard t, IProject p, ITask ta)
        {
            this.taskBoard = t;
            this.project = p;
            this.task = ta;
        }

        public async Task<IActionResult> Index()
        {
            var data = await taskBoard.GetAllTaskBoards();
            return View(data);
        }

        public async Task<IActionResult> AddTaskBoard()
        {
            ViewBag.Projects = await project.GetAllProjects();
            ViewBag.Tasks = await task.GetAllTasks();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTaskBoard(TaskBoards t)
        {
            await taskBoard.AddNewTaskBoard(t);
            TempData["SucMsg"] = "Task Board added successfully!";
            return RedirectToAction("Index");
        }
    }
}