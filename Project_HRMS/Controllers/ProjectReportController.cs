using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;

namespace Project_Hrms.Controllers
{
    public class ProjectReportController : Controller
    {
        private readonly IProjectReportService projectReportService;

        public ProjectReportController(IProjectReportService projectReportService)
        {
            this.projectReportService = projectReportService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await projectReportService.FetchProjectReport();

            return View(data);
        }
    }
}