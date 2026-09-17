using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface
{
    public interface IAttendanceService
    {
        Task<List<Attendance>> GetAllAttendanceAsync();
        Task<List<Attendance>> GetTodayAttendanceAsync();
        Task<Attendance> GetAttendanceByIdAsync(int id);
        Task UpdateAttendanceAsync(Attendance model);
        Task<List<Department>> GetAllDepartmentsAsync();
        Task<List<User>> GetAllEmployeesAsync();
        Task<List<int>> GetApprovedLeaveUserIdsForTodayAsync();
    }
}