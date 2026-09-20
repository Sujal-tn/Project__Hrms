using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly ITimesheetService timesheetService;

        public TimesheetController(ITimesheetService timesheetService)
        {
            this.timesheetService = timesheetService;
        }

        // GET: Timesheet/Index (Employee)
        public async Task<IActionResult> Index(string filter = "All")
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // placeholder until login is wired in

            ViewBag.Projects = new SelectList(await timesheetService.GetAllProjectsAsync(), "ProjectId", "ProjectName");
            ViewBag.Filter = filter;

            var timesheets = await timesheetService.GetTimesheetsByUserAsync(userId, filter);
            return View(timesheets);
        }

        // POST: Timesheet/AddTimesheet (Employee)
        [HttpPost]
        public async Task<IActionResult> AddTimesheet(int projectId, DateTime date, int workHours)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // placeholder until login is wired in

            var timesheet = new Timesheet
            {
                UserId = userId,
                ProjectId = projectId,
                Date = date,
                WorkHours = workHours
            };

            await timesheetService.AddTimesheetAsync(timesheet);

            TempData["success"] = "Timesheet added successfully!";
            return RedirectToAction("Index");
        }

        // POST: Timesheet/SendForApproval (Employee)
        [HttpPost]
        public async Task<IActionResult> SendForApproval(string timesheetIds)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; 

            if (string.IsNullOrEmpty(timesheetIds))
            {
                TempData["error"] = "No timesheets selected for approval.";
                return RedirectToAction("Index");
            }

            var ids = timesheetIds.Split(',').Select(int.Parse).ToList();
            await timesheetService.SendForApprovalAsync(ids, userId);

            TempData["success"] = "Timesheets sent for approval.";
            return RedirectToAction("Index");
        }
    }
}
