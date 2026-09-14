using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Services
{
    public class ProjectService : IProject
    {
        private readonly ApplicationDbContext db;
        public ProjectService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddNewProject(Projects p)
        {

            db.Projects.Add(p);
            db.SaveChanges();
        }

        public List<Projects> GetAllProjects()
        {
            var data = db.Projects.ToList();
            return data;

        }

        public Projects FindProjectById(int id)
        {
            var data = db.Projects.Find(id);
            if (data!= null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public void UpdateProject(Projects p)
        {
            db.Projects.Update(p);
            db.SaveChanges();
        }

        public void DeleteProject(int id)
        {
            var data = db.Projects.Find(id);
            if (data != null)
            {
                db.Projects.Remove(data);
                db.SaveChanges();
            }
        }
    }
}
