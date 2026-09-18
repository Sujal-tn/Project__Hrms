using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface IAttendanceReport
    {
        Task<List<Attendance>> FeatchAttendanceReport();
    }
}
