using Project_Hrms.Models;
using Project_Hrms.Service;
using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;

namespace DemoHRMS.Controllers
{
    public class LeaveReportController : Controller
    {
        private readonly ILeaveReportService leaveReportService;

        public LeaveReportController(ILeaveReportService leaveReportService)
        {
            this.leaveReportService = leaveReportService;
        }
        public async Task<IActionResult> Index()
        {
            var leaves = await leaveReportService.FeatchLeaveRequest();

            var monthlyPaidLeave = leaves
                .Where(x => x.LeaveTypeId == 4 && x.Status == "Approved")
                .GroupBy(x => x.StartDate.ToString("yyyy-MM"))
                .Select(x => new LeaveRequest
                {
                    Month = x.Key,
                    Days = x.Sum(a => a.NumberOfDays)
                })
                .OrderBy(x => x.Month)
                .ToList();

            ViewBag.MonthlyPaidLeave = monthlyPaidLeave;

            return View(leaves);
        }
    }
}
