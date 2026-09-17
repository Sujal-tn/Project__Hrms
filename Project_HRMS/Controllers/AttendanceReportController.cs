using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;

namespace Project_Hrms.Controllers
{
    public class AttendanceReportController : Controller
    {
        private readonly IAttendanceReport attendanceReportService;

        public AttendanceReportController(IAttendanceReport attendanceReportService)
        {
            this.attendanceReportService = attendanceReportService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await attendanceReportService.FeatchAttendanceReport();

            var monthlyAttendance = data
                .GroupBy(x => x.Date.ToString("yyyy-MM"))
                .Select(x => new MonthlyAttendance
                {
                    Month = x.Key,
                    Present = x.Count(a => a.Status == "Present"),
                    Absent = x.Count(a => a.Status == "Absent")
                })
                .ToList();

            ViewBag.MonthlyAttendance = monthlyAttendance;

            return View(data);
        }
    }
}
