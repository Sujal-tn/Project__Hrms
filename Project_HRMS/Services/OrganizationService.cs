using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Migrations;
using Project_Hrms.Models;

namespace Project_Hrms.Services
{
    public class OrganizationService : IOrganizationService
      

    {
        private readonly ApplicationDbContext db;
        public OrganizationService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddOrganization(Organization organization)
        {
            db.Organizations.Add(organization);
            db.SaveChanges();
        }

        public void DeleteOrganization(int id)
        {
            var organization = db.Organizations
                    .Find( id);

            if (organization != null)
            {
                db.Organizations.Remove(organization);
                db.SaveChanges();
            }
        }

        public Organization fetchOrganizationById(int id)
        {
            var organization = db.Organizations
                      .Find(id);
            return organization;

        }

        public List<Organization> GetOrganization()
        {

            var or = db.Organizations.ToList();
            return or;
        }

        public void UpdateOrganization(Organization organization)
        {
            db.Organizations.Update(organization);
            db.SaveChanges();
        }
    }
}
