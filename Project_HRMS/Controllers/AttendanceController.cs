using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;

namespace Project_Hrms.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            this.attendanceService = attendanceService;
        }

        // GET: Attendance/Index (Employee)
        public async Task<IActionResult> Index()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // placeholder until login is wired in

            var todayAttendance = await attendanceService.GetTodayAttendanceAsync(userId);
            var summary = await attendanceService.GetAttendanceSummaryAsync(userId);
            var history = await attendanceService.GetAttendanceHistoryAsync(userId);

            ViewBag.Employee = await attendanceService.GetEmployeeAsync(userId);
            ViewBag.AttendanceStatus = await attendanceService.GetAttendanceStatusAsync(userId);
            ViewBag.TotalToday = summary.TotalToday;
            ViewBag.TotalWeek = summary.TotalWeek;
            ViewBag.TotalMonth = summary.TotalMonth;
            ViewBag.OvertimeMonth = summary.OvertimeMonth;
            ViewBag.TodayAttendance = todayAttendance;

            return View(history);
        }

        // POST: Attendance/MarkAttendance (Employee)
        [HttpPost]
        public async Task<IActionResult> MarkAttendance()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // placeholder until login is wired in

            await attendanceService.MarkAttendanceAsync(userId);
            return RedirectToAction("Index");
        }

        // GET: Attendance/FilterAttendance (Employee)
        public async Task<IActionResult> FilterAttendance(string? status, DateTime? startDate, DateTime? endDate, string sortBy = "recent")
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // placeholder until login is wired in

            var summary = await attendanceService.GetAttendanceSummaryAsync(userId);
            ViewBag.Employee = await attendanceService.GetEmployeeAsync(userId);
            ViewBag.AttendanceStatus = await attendanceService.GetAttendanceStatusAsync(userId);
            ViewBag.TotalToday = summary.TotalToday;
            ViewBag.TotalWeek = summary.TotalWeek;
            ViewBag.TotalMonth = summary.TotalMonth;
            ViewBag.OvertimeMonth = summary.OvertimeMonth;
            ViewBag.TodayAttendance = await attendanceService.GetTodayAttendanceAsync(userId);
            ViewBag.SelectedStatus = status;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.SortBy = sortBy;

            var filtered = await attendanceService.GetFilteredAttendanceAsync(userId, status, startDate, endDate, sortBy);
            return View("Index", filtered);
        }
    }
}
