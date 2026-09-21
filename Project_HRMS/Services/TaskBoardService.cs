using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Services
{
    public class TaskBoardService : ITaskBoard
    {
        private readonly ApplicationDbContext db;
        public TaskBoardService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddNewTaskBoard(TaskBoards t)
        {
            await db.TaskBoards.AddAsync(t);
            await db.SaveChangesAsync();
        }

        public async Task<List<TaskBoards>> GetAllTaskBoards()
        {
            var data = await db.TaskBoards
                .Include(x => x.Project)
                .Include(x => x.Task)
                .ToListAsync();

            return data;
        }
    }
}

