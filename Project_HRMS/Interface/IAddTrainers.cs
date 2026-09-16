using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface IAddTrainers
    {
        Task<List<Trainers>> FetchAll();

        Task<Trainers?> FindByID(int id);

        Task AddTrainer(Trainers t);

        Task UpdateTrainer(Trainers t);

        Task DeleteTrainer(int id);
    }
}
