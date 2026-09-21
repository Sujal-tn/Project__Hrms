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

            var totalWorkingDays = data.Count(x => x.Status == "Present");

            var totalLeaveTaken = data.Count(x => x.Status == "Absent");

            var totalHolidays = data.Count(x => x.Status == "Holiday");

            var totalHalfdays = data.Count(x => x.Status == "Half Day");


            ViewBag.TotalWorkingDays = totalWorkingDays;
            ViewBag.TotalLeaveTaken = totalLeaveTaken;
            ViewBag.TotalHolidays = totalHolidays;
            ViewBag.TotalHalfdays = totalHalfdays;


            var monthlyAttendance = data
                .GroupBy(x => x.Date.ToString("yyyy-MM"))
                .Select(x => new MonthlyAttendance
                {
                    Month = x.Key,
                    Present = x.Count(a => a.Status == "Present"),
                    Absent = x.Count(a => a.Status == "Absent")
                })
                .OrderBy(x => x.Month)
                .ToList();

            ViewBag.MonthlyAttendance = monthlyAttendance; ;

            return View(data);
        }

    }
}
