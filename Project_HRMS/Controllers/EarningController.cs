using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Hrms.Interface.PayrollInterface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers
{
    public class EarningController : Controller
    {
        private readonly IEarningTypeService earningTypeService;
        private readonly IEarningService earningService;

        public EarningController(IEarningTypeService earningTypeService, IEarningService earningService)
        {
            this.earningTypeService = earningTypeService;
            this.earningService = earningService;
        }

        // ---- EarningType ----
        public IActionResult EarningType()
        {
            return View();
        }

        public async Task<IActionResult> FetchEarningType()
        {
            var data = await earningTypeService.GetAllEarningTypesAsync();
            return Json(data.Select(et => new { et.EarntypeId, et.EarningName }));
        }

        [HttpPost]
        public async Task<IActionResult> AddEarningType(EarningType earntype)
        {
            await earningTypeService.AddEarningTypeAsync(earntype);
            return Json(new { success = true });
        }

        public async Task<IActionResult> FindEarningTypeDetails(int id)
        {
            var entity = await earningTypeService.GetEarningTypeByIdAsync(id);
            return Json(entity);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEarnTypeDetails(EarningType et)
        {
            await earningTypeService.UpdateEarningTypeAsync(et);
            return Json(new { success = true });
        }

        public async Task<IActionResult> DeleteEarningType(int id)
        {
            var ok = await earningTypeService.DeleteEarningTypeAsync(id);
            return Json(new { success = ok, message = ok ? null : "EarningType not found." });
        }

        // ---- Earning (percentage mapping) ----
        public async Task<IActionResult> Earning()
        {
            ViewBag.earningType = new SelectList(await earningTypeService.GetAllEarningTypesAsync(), "EarntypeId", "EarningName");
            ViewBag.department = new SelectList(await earningService.GetDepartmentsAsync(), "DepartmentId", "Name");
            ViewBag.designation = new SelectList(await earningService.GetDesignationsAsync(), "DesignationId", "Name");
            return View();
        }

        public async Task<IActionResult> FetchEarning()
        {
            var data = await earningService.GetAllEarningsAsync();
            var result = data.Select(e => new
            {
                e.EarningsId,
                e.EarntypeId,
                EarningType = new { e.EarningType.EarningName },
                e.EarningsPercentage,
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
        public async Task<IActionResult> AddEarning(Earning earning)
        {
            earning.CreatedBy = User.Identity?.Name; // no-op until auth is wired
            await earningService.AddEarningAsync(earning);
            return Json(new { success = true });
        }

        public async Task<IActionResult> FindEarningDetails(int id)
        {
            var entity = await earningService.GetEarningByIdAsync(id);
            if (entity == null) return Json(new { error = "Earning details not found" });

            return Json(new
            {
                earningsId = entity.EarningsId,
                earntypeId = entity.EarntypeId,
                earningsPercentage = entity.EarningsPercentage,
                departmentId = entity.DepartmentId,
                designationId = entity.DesignationId
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEarningDetails(Earning earn)
        {
            earn.ModifiedBy = User.Identity?.Name;
            await earningService.UpdateEarningAsync(earn);
            return Json(new { success = true });
        }

        public async Task<IActionResult> DeleteEarning(int id)
        {
            var ok = await earningService.DeleteEarningAsync(id);
            return Json(new { success = ok, message = ok ? null : "Earning not found." });
        }
    }
}
