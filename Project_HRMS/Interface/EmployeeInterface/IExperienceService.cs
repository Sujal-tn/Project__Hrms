using Project_Hrms.Models;

namespace Project_Hrms.Interface.EmployeeInterface
{
    public interface IExperienceService
    {
        List<Experince> GetExperinceInfo(int id);

        void AddExperinceInfo(Experince b);

        void UpdateExperinceInfo(Experince ba);

        void DeleteExperinceInfo(int id);
    }
}
