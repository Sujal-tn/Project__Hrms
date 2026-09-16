using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Services
{
    public class ProjectService : IProject
    {
        private readonly ApplicationDbContext db;
        public ProjectService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddNewProject(Projects p)
        {
            await db.Projects.AddAsync(p);
            await db.SaveChangesAsync();
        }

        public async Task<List<Projects>> GetAllProjects()
        {
            var data = await db.Projects.ToListAsync();
            return data;
        }

        public async Task<Projects> FindProjectById(int id)
        {
            var data = await db.Projects.FindAsync(id);

            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }
        public async Task UpdateProject(Projects p)
        {
            db.Projects.Update(p);
            await db.SaveChangesAsync();
        }

        public async Task DeleteProject(int id)
        {
            var data = await db.Projects.FindAsync(id);

            if (data != null)
            {
                db.Projects.Remove(data);
                await db.SaveChangesAsync();
            }
        }
    }
}
