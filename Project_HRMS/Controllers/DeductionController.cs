using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Hrms.Interface.PayrollInterface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers
{
    public class DeductionController : Controller
    {
        private readonly IDeductionTypeService deductionTypeService;
        private readonly IDeductionService deductionService;

        public DeductionController(IDeductionTypeService deductionTypeService, IDeductionService deductionService)
        {
            this.deductionTypeService = deductionTypeService;
            this.deductionService = deductionService;
        }

        // ---- DeductionType ----
        public IActionResult DeductionType()
        {
            return View();
        }

        public async Task<IActionResult> FetchDeductionType()
        {
            var data = await deductionTypeService.GetAllDeductionTypesAsync();
            return Json(data.Select(d => new { d.DeductionTypeId, d.DeductionsName }));
        }

        [HttpPost]
        public async Task<IActionResult> AddDeductionType(DeductionType deducttype)
        {
            await deductionTypeService.AddDeductionTypeAsync(deducttype);
            return Json(new { success = true });
        }

        public async Task<IActionResult> FetchDeductionTypeDetails(int id)
        {
            var entity = await deductionTypeService.GetDeductionTypeByIdAsync(id);
            return Json(entity);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDeductTypeDetails(DeductionType de)
        {
            await deductionTypeService.UpdateDeductionTypeAsync(de);
            return Json(new { success = true });
        }

        public async Task<IActionResult> DeleteDeductionType(int id)
        {
            var ok = await deductionTypeService.DeleteDeductionTypeAsync(id);
            return Json(new { success = ok, message = ok ? null : "DeductionType not found." });
        }

        // ---- Deduction (percentage mapping) ----
        public async Task<IActionResult> Deduction()
        {
            ViewBag.deductionType = new SelectList(await deductionTypeService.GetAllDeductionTypesAsync(), "DeductionTypeId", "DeductionsName");
            ViewBag.department = new SelectList(await deductionService.GetDepartmentsAsync(), "DepartmentId", "Name");
            ViewBag.designation = new SelectList(await deductionService.GetDesignationsAsync(), "DesignationId", "Name");
            return View();
        }

        public async Task<IActionResult> FetchDeduction()
        {
            var data = await deductionService.GetAllDeductionsAsync();
            var result = data.Select(e => new
            {
                e.DeductionId,
                e.DeductionTypeId,
                DeductionType = new { e.DeductionType.DeductionsName },
                e.DeductionPercentage,
                e.DepartmentId,
                Department = new { e.Department.DepartmentName },
                e.DesignationId,
                Designation = new { e.Designation.DesignationName },
                e.CreatedBy,
                e.CreatedAt,
                e.ModifiedBy,
                e.ModifiedAt
            });
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDeduction(Deduction deduction)
        {
            deduction.CreatedBy = User.Identity?.Name; // no-op until auth is wired
            await deductionService.AddDeductionAsync(deduction);
            return Json(new { success = true });
        }

        public async Task<IActionResult> FindDeductionDetails(int id)
        {
            var entity = await deductionService.GetDeductionByIdAsync(id);
            if (entity == null) return Json(new { error = "Deduction details not found" });

            return Json(new
            {
                deductionId = entity.DeductionId,
                deductTypeId = entity.DeductionTypeId,
                deductionPercentage = entity.DeductionPercentage,
                departmentId = entity.DepartmentId,
                designationId = entity.DesignationId
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDeductionDetails(Deduction deduct)
        {
            deduct.ModifiedBy = User.Identity?.Name;
            await deductionService.UpdateDeductionAsync(deduct);
            return Json(new { success = true });
        }

        public async Task<IActionResult> DeleteDeduction(int id)
        {
            var ok = await deductionService.DeleteDeductionAsync(id);
            return Json(new { success = ok, message = ok ? null : "Deduction not found." });
        }
    }
}
