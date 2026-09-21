using Project_Hrms.Data;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models;

namespace Project_Hrms.Services.EmployeeService
{
    public class FamilyService : IFamilyService
    {
        private readonly ApplicationDbContext db;
        public FamilyService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddFamilyInfo(FamilyInformation e)
        {
            var fam = new FamilyInformation()
            {
                Name = e.Name,
                Relation = e.Relation,
                DateOfBirth = DateTime.Now.ToString(),
                Phone=e.Phone,
                UserId= e.UserId
            };
            db.FamilyInformations.Add(fam);
            db.SaveChanges();
        }

        public void DeleteFamilyInfo(int id)
        {
            var del = db.FamilyInformations.Find(id);
            if(del != null)
            {
                db.FamilyInformations.Remove(del);
                db.SaveChanges();
            }
        }

        public List<FamilyInformation> GetFamilyInfo(int id)
        {
            var edus = db.FamilyInformations
           .Where(x => x.UserId == id)
           .ToList();
            return edus;

        }

        public void UpdateFamilyInfo(FamilyInformation eu)
        {

            db.FamilyInformations.Update(eu);
            db.SaveChanges();
        }
    }
}
