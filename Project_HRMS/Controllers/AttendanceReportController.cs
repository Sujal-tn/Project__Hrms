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
        public IActionResult Index()
        {
            var data = attendanceReportService.FeatchAttendanceReport();
            return View(data);
        }
    }
}
