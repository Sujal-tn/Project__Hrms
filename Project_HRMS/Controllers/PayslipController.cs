using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Interface.PayrollInterface;

namespace Project_Hrms.Controllers
{
    public class PayslipController : Controller
    {
        private readonly IPayslipService payslipService;
        private readonly IEmpService empService;

        public PayslipController(IPayslipService payslipService, IEmpService empService)
        {
            this.payslipService = payslipService;
            this.empService = empService;
        }

        public async Task<IActionResult> GeneratePayslip(int userId, string? month, int? year)
        {
            month ??= DateTime.Now.ToString("MMMM");
            int selectedYear = year ?? DateTime.Now.Year;
            var data = await payslipService.GetPayslipDataAsync(userId, month, selectedYear);

            if (data == null)
            {
                TempData["AlertMessage"] = "No salary record found for this employee.";
                return RedirectToAction(nameof(GeneratePayslipsForAllUsersByDate));
            }

            return View("PayslipView", data);
        }

        public IActionResult GeneratePayslipsForAllUsersByDate()
        {
            var users = empService.FetchEmp();

            ViewBag.Users = new SelectList(users.Select(u => new { u.UserId, FullName = $"{u.FirstName} {u.LastName}" }), "UserId", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GeneratePayslipsForAllUsersByDate(int userId, string month, int year)
        {
            var ok = await payslipService.GeneratePayslipPdfAsync(userId, month, year);

            TempData[ok ? "SuccessMessage" : "AlertMessage"] = ok ? "Payslip generated successfully." : "Could not generate payslip — check the employee's salary details.";

            return RedirectToAction(nameof(GeneratePayslipsForAllUsersByDate));
        }

        [HttpPost]
        public async Task<IActionResult> GeneratePayslipsForSelectedEmployees([FromBody] BulkPayslipRequest request)
        {
            if (request?.EmployeeIds == null || !request.EmployeeIds.Any())
                return BadRequest(new { message = "No employee IDs provided." });

            var failed = await payslipService.GeneratePayslipsForSelectedUsersAsync(request.EmployeeIds, request.Month, request.Year);

            return Ok(failed.Any() ? new { message = "Payslips generated with some failures.", failedUsers = failed } : new { message = "Payslips have been generated successfully." });
        }

        public async Task<IActionResult> TransactionHistory(string? month, int? year, int? department, int? designation)
        {
            var data = await payslipService.GetTransactionHistoryAsync(month, year, department, designation);

            ViewBag.Departments = await payslipService.GetDepartmentsAsync();
            ViewBag.Designations = await payslipService.GetDesignationsAsync();

            return View(data);
        }

        public IActionResult DownloadPayslip(string payslipPath)
        {
            var payslipsRoot = Path.Combine(HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().WebRootPath, "payslips");
            var fullPath = Path.GetFullPath(payslipPath);

            if (!fullPath.StartsWith(payslipsRoot, StringComparison.OrdinalIgnoreCase) || !System.IO.File.Exists(fullPath))
                return NotFound();

            var bytes = System.IO.File.ReadAllBytes(fullPath);

            return File(bytes, "application/pdf", Path.GetFileName(fullPath));
        }

        public async Task<IActionResult> DeleteTransaction(int payslipId)
        {
            await payslipService.DeletePayslipAsync(payslipId);

            return RedirectToAction(nameof(TransactionHistory));
        }

        public async Task<IActionResult> SelectPayslipMonth(string? month, int? year, int userId)
        {
            var payslips = await payslipService.GetMyPayslipsAsync(userId, month, year);

            ViewBag.AvailableYears = Enumerable.Range(DateTime.Now.Year - 10, 11).Reverse().ToList();
            ViewBag.UserId = userId;

            return View(payslips);
        }
    }

    public class BulkPayslipRequest
    {
        public List<int> EmployeeIds { get; set; } = new();
        public string Month { get; set; }
        public int Year { get; set; }
    }
}