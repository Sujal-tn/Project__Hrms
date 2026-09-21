using Project_Hrms.Models;

namespace Project_Hrms.Interface.EmployeeInterface
{
    public interface IEducationService
    {
        List<EductionDetails> GetEducationInfo(int id);

        void AddEducationInfo(EductionDetails e);

        void UpdateEducationInfo(EductionDetails eu);

        void DeleteEducationInfo(int id);
    }
}
