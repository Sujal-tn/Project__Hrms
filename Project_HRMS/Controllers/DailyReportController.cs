using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;

namespace Project_Hrms.Controllers
{
    public class DailyReportController : Controller
    {
        private readonly IDailyReportService dailyReportService;

        public DailyReportController(IDailyReportService dailyReportService)
        {
            this.dailyReportService = dailyReportService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await dailyReportService.FeatchDailyAttendace();

            var chartData = data
                .GroupBy(x => x.Date.Date)
                .Select(x => new
                {
                    Date = x.Key.ToString("dd-MM-yyyy"),
                    Present = x.Count(a => a.Status == "Present"),
                    Absent = x.Count(a => a.Status == "Absent")
                })
                .OrderBy(x => x.Date)
                .ToList();

            ViewBag.DailyAttendanceChart = chartData;

            return View(data);
        }
    }
}