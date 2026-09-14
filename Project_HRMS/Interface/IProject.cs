using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface IProject
    {
        void AddNewProject(Projects p);

        List<Projects> GetAllProjects();

        Projects FindProjectById(int id);

        void UpdateProject(Projects p);

        void DeleteProject(int id);


    }
}
