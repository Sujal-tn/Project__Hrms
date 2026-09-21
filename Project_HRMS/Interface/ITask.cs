using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface ITask
    {
        Task<int> AddNewTask(Tasks t);
        Task<List<Tasks>> GetAllTasks();
        Task AddTaskMember(TaskMembers member);
    }
}