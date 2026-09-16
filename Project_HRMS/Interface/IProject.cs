using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface IProject
    {
        Task AddNewProject(Projects p);

        Task<List<Projects>> GetAllProjects();

        Task<Projects> FindProjectById(int id);

        Task UpdateProject(Projects p);

        Task DeleteProject(int id);
    }
}