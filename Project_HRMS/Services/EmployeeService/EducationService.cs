using Project_Hrms.Data;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models;

namespace Project_Hrms.Services.EmployeeService
{
    public class EducationService : IEducationService
    {
        private readonly ApplicationDbContext db;
        public EducationService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddEducationInfo(EductionDetails e)
        {
            var edu = new EductionDetails()
            {
                EducationType = e.EducationType,
                UniversityName = e.UniversityName,
                Startdate = e.Startdate,
                EndDate = e.EndDate,
                UserId = e.UserId
            };

            db.EductionDetails.Add(edu);
            db.SaveChanges();
        }

        public void DeleteEducationInfo(int id)
        {
            var del = db.EductionDetails.Find(id);
            if(del != null)
                {
                db.EductionDetails.Remove(del);
                db.SaveChanges();
            }
        }

        public List<EductionDetails> GetEducationInfo(int id)
        {
            var edus = db.EductionDetails
          .Where(x => x.UserId == id)
          .ToList();

            return edus;
        }

        public void UpdateEducationInfo(EductionDetails eu)
        {

            db.EductionDetails.Update(eu);
            db.SaveChanges();
        }
    }
}
