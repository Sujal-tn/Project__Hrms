using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface
{
    public interface IAttendanceService
    {
        //Admin Attendance
        Task<List<Attendance>> GetAllAttendanceAsync();
        Task<List<Attendance>> GetTodayAttendanceAsync();
        Task<Attendance> GetAttendanceByIdAsync(int id);
        Task UpdateAttendanceAsync(Attendance model);
        Task<List<Department>> GetAllDepartmentsAsync();
        Task<List<User>> GetAllEmployeesAsync();
        Task<List<int>> GetApprovedLeaveUserIdsForTodayAsync();

        //Employee Attendance
        Task<Attendance?> GetTodayAttendanceAsync(int userId);
        Task<string> GetAttendanceStatusAsync(int userId);
        Task MarkAttendanceAsync(int userId);
        Task<List<Attendance>> GetAttendanceHistoryAsync(int userId);
        Task<User?> GetEmployeeAsync(int userId);
        Task<List<Attendance>> GetFilteredAttendanceAsync(int userId, string? status, DateTime? startDate, DateTime? endDate, string sortBy);
        Task<(decimal TotalToday, decimal TotalWeek, decimal TotalMonth, decimal OvertimeMonth)> GetAttendanceSummaryAsync(int userId);
    }
}