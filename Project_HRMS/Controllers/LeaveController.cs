using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ILeaveService leaveService;

        public LeaveController(ILeaveService leaveService)
        {
            this.leaveService = leaveService;
        }

        /* ---- Admin Leave Management ---- */
        // GET: Leave/AddLeaveType
        public async Task<IActionResult> AddLeaveType()
        {
            var leaveTypes = await leaveService.GetAllLeaveTypesAsync();
            return View(leaveTypes);
        }

        // POST: Leave/AddLeaveType
        [HttpPost]
        public async Task<IActionResult> AddLeaveType(string LeaveType)
        {
            if (string.IsNullOrWhiteSpace(LeaveType))
            {
                TempData["error"] = "Leave type cannot be empty!";
                return RedirectToAction("AddLeaveType");
            }

            if (await leaveService.LeaveTypeExistsAsync(LeaveType))
            {
                TempData["error"] = "Leave type already exists!";
                return RedirectToAction("AddLeaveType");
            }

            await leaveService.AddLeaveTypeAsync(new MasterLeaveType { LeaveType = LeaveType });

            TempData["success"] = "Leave type added successfully!";
            return RedirectToAction("AddLeaveType");
        }

        // POST: Leave/DeleteLeaveType
        [HttpPost]
        public async Task<IActionResult> DeleteLeaveType(int leaveTypeId)
        {
            try
            {
                var leaveType = await leaveService.GetLeaveTypeByIdAsync(leaveTypeId);
                if (leaveType == null)
                {
                    TempData["error"] = "Leave type not found.";
                    return RedirectToAction("AddLeaveType");
                }

                await leaveService.DeleteLeaveTypeAsync(leaveTypeId);
                TempData["success"] = "Leave type deleted successfully!";
            }
            catch (DbUpdateException)
            {
                TempData["error"] = "Cannot delete this leave type because it is being used in another record.";
            }
            catch (Exception)
            {
                TempData["error"] = "An error occurred while deleting the leave type.";
            }

            return RedirectToAction("AddLeaveType");
        }

        // GET: Leave/AddLeaveDeptwise
        public async Task<IActionResult> AddLeaveDeptwise()
        {
            ViewBag.Departments = new SelectList(await leaveService.GetAllDepartmentsAsync(), "DepartmentId", "DepartmentName");
            ViewBag.LeaveTypes = new SelectList(await leaveService.GetAllLeaveTypesAsync(), "LeaveTypeId", "LeaveType");

            return View();
        }

        // POST: Leave/AddLeaveDeptwise
        [HttpPost]
        public async Task<IActionResult> AddLeaveDeptwise(int departmentId, int leaveTypeId, int leavesCount)
        {
            var status = await leaveService.GetLeaveTypeStatusAsync(leaveTypeId);

            if (status == "Inactive")
            {
                TempData["error"] = "The selected leave type is deactivated and cannot be allocated.";
                return RedirectToAction("AddLeaveDeptwise");
            }

            await leaveService.AllocateLeaveDeptwiseAsync(departmentId, leaveTypeId, leavesCount);

            TempData["success"] = "Leave allocation for the department has been updated successfully!";
            return RedirectToAction("AddLeaveDeptwise");
        }

        // GET: Leave/DepartmentLeaveDetails
        public async Task<IActionResult> DepartmentLeaveDetails()
        {
            ViewBag.Departments = new SelectList(await leaveService.GetAllDepartmentsAsync(), "DepartmentId", "DepartmentName");
            var departmentLeaves = await leaveService.GetDepartmentLeavesAsync(null);
            return View(departmentLeaves);
        }

        // POST: Leave/DepartmentLeaveDetails (filter by department)
        [HttpPost]
        public async Task<IActionResult> DepartmentLeaveDetails(int departmentId)
        {
            ViewBag.Departments = new SelectList(await leaveService.GetAllDepartmentsAsync(), "DepartmentId", "DepartmentName");
            var departmentLeaves = await leaveService.GetDepartmentLeavesAsync(departmentId);
            return View(departmentLeaves);
        }

        // POST: Leave/DeleteLeave (trash icon on Department Leave Details page)
        [HttpPost]
        public async Task<IActionResult> DeleteLeave(int id)
        {
            await leaveService.DeleteDepartmentLeaveAsync(id);
            TempData["success"] = "Department leave record deleted successfully!";
            return RedirectToAction("DepartmentLeaveDetails");
        }

        // GET: Leave/ManageLeaveSettings
        public async Task<IActionResult> ManageLeaveSettings()
        {
            var leaveTypes = await leaveService.GetAllLeaveTypesAsync();
            return View(leaveTypes);
        }

        // POST: Leave/UpdateLeaveTypeStatus
        [HttpPost]
        public async Task<IActionResult> UpdateLeaveTypeStatus(int leaveTypeId, string status)
        {
            await leaveService.UpdateLeaveTypeStatusAsync(leaveTypeId, status);
            TempData["success"] = $"Leave type status has been updated to {status} successfully!";
            return RedirectToAction("ManageLeaveSettings");
        }

        /* ---- Employee Leave Management ---- */
        // GET: Leave/LeaveRequests (Employee)
        public async Task<IActionResult> LeaveRequests()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;

            ViewBag.LeaveTypes = await leaveService.GetDepartmentLeaveTypesForUserAsync(userId);
            ViewBag.LeaveBalances = await leaveService.GetLeaveBalancesByUserAsync(userId);

            var leaveRequests = await leaveService.GetLeaveRequestsByUserAsync(userId);
            return View(leaveRequests);
        }

        // POST: Leave/ApplyLeave (Employee)
        [HttpPost]
        public async Task<IActionResult> ApplyLeave(int leaveTypeId, DateTime startDate, DateTime endDate, string reason)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; 

            try
            {
                var request = new LeaveRequest
                {
                    UserId = userId,
                    LeaveTypeId = leaveTypeId,
                    StartDate = startDate,
                    EndDate = endDate,
                    Reason = reason
                };

                await leaveService.ApplyLeaveAsync(request);
                TempData["success"] = "Leave request submitted successfully!";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToAction("LeaveRequests");
        }

        /* ---- Manager Leave Approval ---- */
        // GET: Leave/ViewLeaveRequests (Manager)
        public async Task<IActionResult> ViewLeaveRequests()
        {
            int managerId = HttpContext.Session.GetInt32("UserId") ?? 0;

            var leaveRequests = await leaveService.GetLeaveRequestsForManagerAsync(managerId);
            return View(leaveRequests);
        }

        // POST: Leave/UpdateLeaveRequest (Manager - Approve/Reject)
        [HttpPost]
        public async Task<IActionResult> UpdateLeaveRequest(int LeaveRequestId, string action)
        {
            string approvedBy = HttpContext.Session.GetString("UserName") ?? "Manager";

            try
            {
                await leaveService.UpdateLeaveRequestStatusAsync(LeaveRequestId, action, approvedBy);
                TempData["success"] = $"Leave request has been {action.ToLower()}ed successfully!";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToAction("ViewLeaveRequests");
        }
    }
}
