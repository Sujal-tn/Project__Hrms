using Project_Hrms.Interface;

namespace Project_Hrms.Services.Training
{
    public class AddTrainerServicescs : IAddTrainers
    {
        private readonly ApplicationDbContext db;
        public AddTrainerServicescs(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddTrainer(Trainers t)
        {
            await db.Trainers.AddAsync(t);
            await db.SaveChangesAsync();
        }

        public async Task DeleteTrainer(int id)
        {
            var data = await db.Trainers.FindAsync(id);
            if (data != null)
            {
                db.Trainers.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Trainers>> FetchAll()
        {
            var data = await db.Trainers.ToListAsync();
            return data;
        }

        public async Task<Trainers?> FindByID(int id)
        {
            var data = await db.Trainers.FindAsync(id);
            return data;
        }

        public async Task UpdateTrainer(Trainers t)
        {
            db.Trainers.Update(t);
            await db.SaveChangesAsync();
        }


    }
}
