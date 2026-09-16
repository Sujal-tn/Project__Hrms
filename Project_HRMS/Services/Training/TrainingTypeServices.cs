using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Services
{
    public class TrainingTypeServices : ITrainingType
    {
        private readonly ApplicationDbContext db;
        public TrainingTypeServices(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task AddTrainingType(TrainingType ttype)
        {
            await db.TrainingTypes.AddAsync(ttype);
            await db.SaveChangesAsync();
        }

        public async Task DeleteTrainingType(int id)
        {
            var data = await db.TrainingTypes.FindAsync(id);
            if (data != null)
            {
                db.TrainingTypes.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<TrainingType>> FetchAll()
        {
            var data = await db.TrainingTypes.ToListAsync();
            return data;
        }


        public async Task<TrainingType?> FindByID(int id)
        {
            var data = await db.TrainingTypes.FindAsync(id);
            return data;
        }

        public async Task UpdateTrainingType(TrainingType ttype)
        {
            db.TrainingTypes.Update(ttype);
            await db.SaveChangesAsync();
        }
    }
}
