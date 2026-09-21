using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Services
{
    public class TaskService : ITask
    {
        private readonly ApplicationDbContext data;
        public TaskService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<int> AddNewTask(Tasks t)
        {
            await data.Tasks.AddAsync(t);
            await data.SaveChangesAsync();

            return t.TaskId;
        }

        public async Task AddTaskMember(TaskMembers member)
        {
            await data.TaskMembers.AddAsync(member);
            await data.SaveChangesAsync();
        }

        public async Task<List<Tasks>> GetAllTasks()
        {
            var taskList = await data.Tasks
                .Include(t => t.Projects)
                .Include(t => t.Taskmember)
                .ThenInclude(x => x.User)
                .ToListAsync();

            return taskList;
        }

        public async Task<Tasks> FindTaskById(int id)
        {
            var tasklist = await data.Tasks.FindAsync(id);

            if (tasklist != null)
            {
                return tasklist;
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateTask(Tasks t)
        {
            data.Tasks.Update(t);
            await data.SaveChangesAsync();
        }

        public async Task DeleteTask(int id)
        {
            var tasklist = await data.Tasks.FindAsync(id);

            if (tasklist != null)
            {
                data.Tasks.Remove(tasklist);
                await data.SaveChangesAsync();
            }
        }
    }
}