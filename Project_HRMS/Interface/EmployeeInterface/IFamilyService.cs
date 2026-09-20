using Project_Hrms.Models;

namespace Project_Hrms.Interface.EmployeeInterface
{
    public interface IFamilyService
    {
        List<FamilyInformation> GetFamilyInfo(int id);

        void AddFamilyInfo(FamilyInformation e);

        void UpdateFamilyInfo(FamilyInformation eu);

        void DeleteFamilyInfo(int id);
    }
}
