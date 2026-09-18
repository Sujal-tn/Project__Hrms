using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly ApplicationDbContext db;

        public LeaveService(ApplicationDbContext db)
        {
            this.db = db;
        }

        // ---------- Leave Type ----------
        public async Task<List<MasterLeaveType>> GetAllLeaveTypesAsync()
        {
            return await db.MasterLeaveType.ToListAsync();
        }

        public async Task<bool> LeaveTypeExistsAsync(string leaveType)
        {
            return await db.MasterLeaveType
                .AnyAsync(l => l.LeaveType.ToLower() == leaveType.ToLower());
        }

        public async Task AddLeaveTypeAsync(MasterLeaveType leaveType)
        {
            leaveType.Status = "Active";
            await db.MasterLeaveType.AddAsync(leaveType);
            await db.SaveChangesAsync();
        }

        public async Task<MasterLeaveType?> GetLeaveTypeByIdAsync(int leaveTypeId)
        {
            return await db.MasterLeaveType.FindAsync(leaveTypeId);
        }

        public async Task DeleteLeaveTypeAsync(int leaveTypeId)
        {
            var leaveType = await db.MasterLeaveType.FindAsync(leaveTypeId);
            if (leaveType != null)
            {
                db.MasterLeaveType.Remove(leaveType);
                await db.SaveChangesAsync();
            }
        }

        // ---- Department-wise Allocation ----
        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            return await db.Departments.ToListAsync();
        }

        public async Task<string?> GetLeaveTypeStatusAsync(int leaveTypeId)
        {
            return await db.MasterLeaveType
                .Where(l => l.LeaveTypeId == leaveTypeId)
                .Select(l => l.Status)
                .FirstOrDefaultAsync();
        }

        public async Task AllocateLeaveDeptwiseAsync(int departmentId, int leaveTypeId, int leavesCount)
        {
            var departmentLeave = await db.DepartmentLeaves
                .FirstOrDefaultAsync(dl => dl.DepartmentId == departmentId && dl.LeaveTypeId == leaveTypeId);

            if (departmentLeave == null)
            {
                departmentLeave = new DepartmentLeaves
                {
                    DepartmentId = departmentId,
                    LeaveTypeId = leaveTypeId,
                    LeavesCount = leavesCount,
                    Status = "Active"
                };
                await db.DepartmentLeaves.AddAsync(departmentLeave);
            }
            else
            {
                departmentLeave.LeavesCount += leavesCount;
                db.DepartmentLeaves.Update(departmentLeave);
            }

            await db.SaveChangesAsync();

            var departmentEmployees = await db.Users
                .Where(u => u.DepartmentId == departmentId)
                .ToListAsync();

            foreach (var employee in departmentEmployees)
            {
                var leaveBalance = await db.LeaveBalance
                    .FirstOrDefaultAsync(lb => lb.UserId == employee.UserId && lb.LeaveTypeId == leaveTypeId);

                if (leaveBalance == null)
                {
                    leaveBalance = new LeaveBalance
                    {
                        UserId = employee.UserId,
                        DepartmentLeavesId = departmentLeave.DepartmentLeavesId,
                        LeaveTypeId = leaveTypeId,
                        TotalLeaves = leavesCount,
                        UsedLeaves = 0
                    };
                    await db.LeaveBalance.AddAsync(leaveBalance);
                }
                else
                {
                    leaveBalance.TotalLeaves += leavesCount;
                    db.LeaveBalance.Update(leaveBalance);
                }
            }

            await db.SaveChangesAsync();
        }

        // ----- Department Leave Details -----
        public async Task<List<DepartmentLeaves>> GetDepartmentLeavesAsync(int? departmentId)
        {
            var query = db.DepartmentLeaves
                .Include(dl => dl.Department)
                .Include(dl => dl.MasterLeaveType)
                .AsQueryable();

            if (departmentId.HasValue && departmentId.Value > 0)
            {
                query = query.Where(dl => dl.DepartmentId == departmentId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task DeleteDepartmentLeaveAsync(int departmentLeavesId)
        {
            var departmentLeave = await db.DepartmentLeaves.FindAsync(departmentLeavesId);
            if (departmentLeave != null)
            {
                db.DepartmentLeaves.Remove(departmentLeave);
                await db.SaveChangesAsync();
            }
        }

        // ----- Leave Settings -----
        public async Task UpdateLeaveTypeStatusAsync(int leaveTypeId, string status)
        {
            var leaveType = await db.MasterLeaveType.FindAsync(leaveTypeId);
            if (leaveType != null)
            {
                leaveType.Status = status;
                db.MasterLeaveType.Update(leaveType);
                await db.SaveChangesAsync();
            }
        }

        // ---------- Employee: Leave Requests -----
        public async Task<List<LeaveBalance>> GetLeaveBalancesByUserAsync(int userId)
        {
            return await db.LeaveBalance
                .Include(lb => lb.MasterLeaveType)
                .Where(lb => lb.UserId == userId && lb.MasterLeaveType.Status == "Active")
                .ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsByUserAsync(int userId)
        {
            return await db.LeaveRequest
                .Include(lr => lr.MasterLeaveType)
                .Where(lr => lr.UserId == userId)
                .OrderByDescending(lr => lr.LeaveRequestId)
                .ToListAsync();
        }

        public async Task ApplyLeaveAsync(LeaveRequest request)
        {
            var leaveType = await db.MasterLeaveType.FindAsync(request.LeaveTypeId);
            if (leaveType == null || leaveType.Status != "Active")
            {
                throw new InvalidOperationException("The selected leave type is not available.");
            }

            var user = await db.Users.FindAsync(request.UserId);
            if (user == null || user.DepartmentId == null)
            {
                throw new InvalidOperationException("Employee's department is not set.");
            }

            var departmentLeave = await db.DepartmentLeaves
                .FirstOrDefaultAsync(dl => dl.DepartmentId == user.DepartmentId && dl.LeaveTypeId == request.LeaveTypeId);

            if (departmentLeave == null)
            {
                throw new InvalidOperationException("This leave type has not been allocated to your department yet.");
            }

            var leaveBalance = await db.LeaveBalance
                .FirstOrDefaultAsync(lb => lb.UserId == request.UserId && lb.LeaveTypeId == request.LeaveTypeId);

            if (leaveBalance == null)
            {
                // First time this employee is using this leave type — initialize from the department's allocation
                leaveBalance = new LeaveBalance
                {
                    UserId = request.UserId,
                    DepartmentLeavesId = departmentLeave.DepartmentLeavesId,
                    LeaveTypeId = request.LeaveTypeId,
                    TotalLeaves = departmentLeave.LeavesCount,
                    UsedLeaves = 0
                };
                await db.LeaveBalance.AddAsync(leaveBalance);
            }

            request.NumberOfDays = (request.EndDate.Date - request.StartDate.Date).Days + 1;

            if (request.NumberOfDays <= 0)
            {
                throw new ArgumentException("End date must be on or after the start date.");
            }

            leaveBalance.ApplyLeave(request.NumberOfDays); // throws if insufficient balance

            request.Status = "Pending";
            request.ApprovedBy = string.Empty;
            request.StatusHistory = $"Applied on {DateTime.Now:dd-MM-yyyy}";

            await db.LeaveRequest.AddAsync(request);
            db.LeaveBalance.Update(leaveBalance);

            await db.SaveChangesAsync();
        }

        public async Task<List<DepartmentLeaves>> GetDepartmentLeaveTypesForUserAsync(int userId)
        {
            var user = await db.Users.FindAsync(userId);
            if (user == null || user.DepartmentId == null)
            {
                return new List<DepartmentLeaves>();
            }

            return await db.DepartmentLeaves
                .Include(dl => dl.MasterLeaveType)
                .Where(dl => dl.DepartmentId == user.DepartmentId && dl.MasterLeaveType.Status == "Active")
                .ToListAsync();
        }
    }
}
