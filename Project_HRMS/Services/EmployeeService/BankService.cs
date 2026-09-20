using Project_Hrms.Data;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models;

namespace Project_Hrms.Services.EmployeeService
{
    public class BankService : IBankInformationService
    {
        private readonly ApplicationDbContext db;

        public BankService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddBankInfo(BankInformation b)
        {
            var ban = new BankInformation()
            {
                BankName= b.BankName,
                AccountNumber= b.AccountNumber,
                IFSCNumber= b.IFSCNumber,
                UserId= b.UserId,
            };
            db.BankInformations.Add(ban);
            db.SaveChanges();
        }

        public void DeleteBankInfo(int id)
        {
            var del = db.BankInformations.Find(id);
            if (del != null)
            {
                db.BankInformations.Remove(del);
                db.SaveChanges();
            }

        }

        public BankInformation GetBankInfo(int id)
        {
            var bank = db.BankInformations.Find(id);
            return bank;
        }

        public void UpdateBankInfo(BankInformation ba)
        {
            db.BankInformations.Update(ba);
            db.SaveChanges();
        }
    }
}
