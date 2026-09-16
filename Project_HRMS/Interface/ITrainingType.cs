using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface ITrainingType
    {
        Task<List<TrainingType>> FetchAll();

        Task<TrainingType?> FindByID(int id);

        Task AddTrainingType(TrainingType ttype);

        Task UpdateTrainingType(TrainingType ttype);

        Task DeleteTrainingType(int id);
    }
}
