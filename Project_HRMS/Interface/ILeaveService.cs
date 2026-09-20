using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface
{
    public interface ILeaveService
    {
        // ---- Leave Type (Add Leave Type page) ----
        Task<List<MasterLeaveType>> GetAllLeaveTypesAsync();
        Task<bool> LeaveTypeExistsAsync(string leaveType);
        Task AddLeaveTypeAsync(MasterLeaveType leaveType);
        Task<MasterLeaveType?> GetLeaveTypeByIdAsync(int leaveTypeId);
        Task DeleteLeaveTypeAsync(int leaveTypeId);

        // ---- Department-wise Leave Allocation (Add Leave page) ----
        Task<List<Department>> GetAllDepartmentsAsync();
        Task<string?> GetLeaveTypeStatusAsync(int leaveTypeId);
        Task AllocateLeaveDeptwiseAsync(int departmentId, int leaveTypeId, int leavesCount);

        // ---- Department Leave Details page ----
        Task<List<DepartmentLeaves>> GetDepartmentLeavesAsync(int? departmentId);
        Task DeleteDepartmentLeaveAsync(int departmentLeavesId);

        // ---- Manage Leave Settings page ----
        Task UpdateLeaveTypeStatusAsync(int leaveTypeId, string status);

        // ---- Employee: Leave Requests page ----
        Task<List<LeaveBalance>> GetLeaveBalancesByUserAsync(int userId);
        Task<List<LeaveRequest>> GetLeaveRequestsByUserAsync(int userId);
        Task ApplyLeaveAsync(LeaveRequest request);
        Task<List<DepartmentLeaves>> GetDepartmentLeaveTypesForUserAsync(int userId);

        // ---- Manager: Leave Approval ----
        Task<List<LeaveRequest>> GetLeaveRequestsForManagerAsync(int managerId);
        Task UpdateLeaveRequestStatusAsync(int leaveRequestId, string action, string approvedBy);
    }
}
