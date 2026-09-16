using Project_Hrms.Data;

namespace Project_Hrms.Services.Training
{
    public class AddTrainingListServices : ITrainingList
    {
        private readonly ApplicationDbContext db;
        public AddTrainingListServices(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddTrainingList(Trainings t)
        {
            await db.Trainings.AddAsync(t);
            await db.SaveChangesAsync();
        }

        public async Task DeleteTrainingList(int id)
        {
            var data = await db.Trainings.FindAsync(id);
            if (data != null)
            {
                db.Trainings.Remove(data);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Trainings>> FetchAll()
        {
            var data = await db.Trainings.ToListAsync();
            return data;
        }

        public async Task<Trainings?> FindByID(int id)
        {
            var data = await db.Trainings.FindAsync(id);
            return data;
        }

        public async Task UpdateTrainingList (Trainings t)
        {
             db.Trainings.Update(t);
            await db.SaveChangesAsync();
        }

        public async Task<List<Trainers>> FetchAllTrainers()
        {
            var data = await db.Trainers.ToListAsync();
            return data;
        }
        public async Task<List<TrainingType>> FetchAllTrainingTypes()
        {
            var data = await db.TrainingTypes.ToListAsync();
            return data;
        }
        public async Task<List<User>> FetchAllUsers()
        {
            var data = await db.Users.ToListAsync();
            return data;
        }

    }
}
