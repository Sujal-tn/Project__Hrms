using Project_Hrms.Models;

namespace Project_Hrms.Interface.EmployeeInterface
{
    public interface IBankInformationService
    {
       BankInformation GetBankInfo(int id);

        void AddBankInfo(BankInformation b);

        void UpdateBankInfo(BankInformation ba);

        void DeleteBankInfo(int id);
    }
}
