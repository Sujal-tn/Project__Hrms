using Project_Hrms.Data;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models;

namespace Project_Hrms.Services.EmployeeService
{
    public class ExperienceService : IExperienceService
    {
        private readonly ApplicationDbContext db;

        public ExperienceService(ApplicationDbContext db)
        {
            this.db= db;
        }
        public void AddExperinceInfo(Experince b)
        {

            var ex = new Experince()
            {
                Designation = b.Designation,
                FromDate = b.FromDate,
                ToDate = b.ToDate,
                UserId= b.UserId,
            };
            db.Experinces.Add(ex);
            db.SaveChanges();
        }

        public void DeleteExperinceInfo(int id)
        {

            var del = db.Experinces.Find(id);
            if (del != null)
            {
                db.Experinces.Remove(del);
                db.SaveChanges();
            }
        }

        public List<Experince> GetExperinceInfo(int id)
        {

            var edus = db.Experinces
           .Where(x => x.UserId == id)
           .ToList();

            return edus;
        }

        public void UpdateExperinceInfo(Experince ba)
        {
            db.Experinces.Update(ba);
            db.SaveChanges();
        }
    }
}
