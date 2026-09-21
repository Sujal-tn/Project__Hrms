using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;

namespace Project_Hrms.Controllers
{
    public class PaySlipsReportController : Controller
    {
        private readonly IPaySlipsReportService paySlipsReportService;

        public PaySlipsReportController(IPaySlipsReportService paySlipsReportService)
        {
            this.paySlipsReportService = paySlipsReportService;
        }

        public async Task<IActionResult> Index()
        {
            var payslips = await paySlipsReportService.FeatchPaySlips();

            ViewBag.TotalSalary = payslips.Sum(x => x.TotalSalary);
            ViewBag.TotalDeductions = payslips.Sum(x => x.Deductions);
            ViewBag.Earnings = payslips.Sum(x => x.Earnings);
            ViewBag.NetSalary = payslips.Sum(x => x.NetPay);

            return View(payslips);
        }

        public async Task<IActionResult> GetSalaryGraphData()
        {
            var data = await paySlipsReportService.GetSalaryGraphData();

            return Json(data);
        }

        public async Task<IActionResult> ExportToPDF()
        {
            var file = await paySlipsReportService.ExportToPDF();

            return File(file, "application/pdf", "PayslipReport.pdf");
        }

        public async Task<IActionResult> ExportToExcel()
        {
            var file = await paySlipsReportService.ExportToExcel();

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "PayslipReport.xlsx"
            );
        }
    }
}
