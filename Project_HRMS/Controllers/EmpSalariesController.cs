using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Interface.PayrollInterface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers
{
    public class EmpSalariesController : Controller
    {
        private readonly IEmployeeSalaryService salaryService;
        private readonly IEmpService empService;

        public EmpSalariesController(IEmployeeSalaryService salaryService, IEmpService empService)
        {
            this.salaryService = salaryService;
            this.empService = empService;
        }

        public async Task<IActionResult> EmployeeSalary(string? designation, string? dateRange, string? sortBy)
        {
            var data = await salaryService.GetAllEmployeeSalariesAsync(designation, dateRange, sortBy);
            return View(data);
        }

        public async Task<IActionResult> AddEmployeeSalary()
        {
            var users = empService.FetchEmp();
            ViewBag.Users = new SelectList(
                users.Select(u => new { u.UserId, FullName = $"{u.FirstName} {u.LastName}" }),
                "UserId", "FullName");

            var (earnings, deductions) = await salaryService.GetAllActiveRulesAsync();
            ViewBag.Earnings = earnings;
            ViewBag.Deductions = deductions;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeSalary(EmployeeSalaries e, List<EmployeeEarnings> Earnings, List<EmployeeDeductions> Deductions)
        {
            await salaryService.AddEmployeeSalaryAsync(e, Earnings ?? new(), Deductions ?? new());
            TempData["SuccessMessage"] = "Salary Added Successfully!!";
            return RedirectToAction(nameof(AddEmployeeSalary));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var salary = await salaryService.GetEmployeeSalaryByIdAsync(id);
            if (salary == null) return NotFound();
            return View(salary);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await salaryService.DeleteEmployeeSalaryAsync(id);
            return RedirectToAction(nameof(EmployeeSalary));
        }
    }
}