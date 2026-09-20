using Project_Hrms.Interface;
using Project_Hrms.Models;
using Project_Hrms.Data;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Services
{
    public class EventService : IEvent
    {
        private readonly ApplicationDbContext d;

        public EventService(ApplicationDbContext db)
        {
            this.d = db;
        }

        public async Task AddNewEvent(Events e)
        {
            await d.Events.AddAsync(e);
            await d.SaveChangesAsync();
        }

        public async Task<List<Events>> GetAllEvents()
        {
            var data = await d.Events.ToListAsync();
            return data;
        }

        public async Task<Events> FindEventById(int id)
        {
            var data = await d.Events.FindAsync(id);
            return data;
        }

        public async Task UpdateEvent(Events e)
        {
            d.Events.Update(e);
            await d.SaveChangesAsync();
        }

        public async Task DeleteEvent(int id)
        {
            var data = await d.Events.FindAsync(id);

            if (data != null)
            {
                d.Events.Remove(data);
                await d.SaveChangesAsync();
            }
        }
    }
}