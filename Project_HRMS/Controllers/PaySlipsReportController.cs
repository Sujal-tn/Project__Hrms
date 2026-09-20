using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;

namespace Project_Hrms.Controllers
{
    public class PaySlipsReportController : Controller
    {
        private readonly IPaySlipsReportService paySlipsService;

        public PaySlipsReportController(IPaySlipsReportService paySlipsService)
        {
            this.paySlipsService = paySlipsService;
        }

        public async Task<IActionResult> Index()
        {
            var slips = await paySlipsService.FeatchPaySlips();
            return View(slips);
        }
    }
}
