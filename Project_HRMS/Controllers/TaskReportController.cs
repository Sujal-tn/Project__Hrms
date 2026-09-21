using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;

namespace Project_Hrms.Controllers
{
    public class TaskReportController : Controller
    {
        private readonly ITaskReportService taskReportService;

        public TaskReportController(ITaskReportService taskReportService)
        {
            this.taskReportService = taskReportService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await taskReportService.FetchTaskReport();

            var totalTasks = data?.Count ?? 0;

            var completedTasks = data?
                .Count(x => x.Status == "Completed") ?? 0;

            var pendingTasks = data?
                .Count(x => x.Status == "Pending") ?? 0;

            var otherTasks = totalTasks - completedTasks - pendingTasks;

            ViewBag.TotalTasks = totalTasks;
            ViewBag.CompletedTasks = completedTasks;
            ViewBag.PendingTasks = pendingTasks;
            ViewBag.OtherTasks = otherTasks;

            var statusChart = data?
                .GroupBy(x => x.Status)
                .Select(x => new
                {
                    Status = x.Key,
                    Count = x.Count()
                })
                .ToList();

            ViewBag.StatusChart = statusChart;

            return View(data);
        }
    }
}