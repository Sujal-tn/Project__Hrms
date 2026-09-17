using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface ITask
    {
        Task AddNewTask(Tasks t);

        Task<List<Tasks>> GetAllTasks();

        Task<Tasks> FindTaskById(int id);

        Task UpdateTask(Tasks t);

        Task DeleteTask(int id);
    }
}
