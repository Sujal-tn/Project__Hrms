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

            ViewBag.TotalLeaves = leaves.Count();

            ViewBag.ApprovedLeaves = leaves
                .Count(x => x.Status == "Approved");

            ViewBag.PendingRequests = leaves
                .Count(x => x.Status == "Pending");

            ViewBag.RejectedLeaves = leaves
                .Count(x => x.Status == "Rejected");


            var monthlyPaidLeave = leaves
                .Where(x => x.Status == "Approved")
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