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

            return View(data);
        }
    }
}