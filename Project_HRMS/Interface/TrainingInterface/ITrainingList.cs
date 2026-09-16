using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface.TrainingInterface
{
    public interface ITrainingList
    {
        Task<List<Trainings>> FetchAll();

        Task<Trainings?> FindByID(int id);

        Task AddTrainingList(Trainings t);

        Task UpdateTrainingList(Trainings  t);

        Task DeleteTrainingList(int id);

        Task<List<Trainers>> FetchAllTrainers();

        Task<List<TrainingType>> FetchAllTrainingTypes();

        Task<List<User>> FetchAllUsers();
    }
}
        