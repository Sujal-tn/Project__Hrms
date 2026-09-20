using Project_Hrms.Interface;
using Project_Hrms.Models;
using Project_Hrms.Data;
using Microsoft.EntityFrameworkCore;

namespace Project_Hrms.Services
{
    public class MasterEventService : IMasterEvent
    {
        private readonly ApplicationDbContext d;

        public MasterEventService(ApplicationDbContext db)
        {
            this.d = db;
        }

        public async Task AddNewMasterEvent(MasterEvents e)
        {
            await d.MasterEvents.AddAsync(e);
            await d.SaveChangesAsync();
        }

        public async Task<List<MasterEvents>> GetAllMasterEvents()
        {
            var data = await d.MasterEvents.ToListAsync();
            return data;
        }

        public async Task DeleteMasterEvent(int id)
        {
            var data = await d.MasterEvents.FindAsync(id);

            if (data != null)
            {
                d.MasterEvents.Remove(data);
                await d.SaveChangesAsync();
            }
        }
    }
}