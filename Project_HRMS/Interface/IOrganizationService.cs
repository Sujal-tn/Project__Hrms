using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface IOrganizationService
    {
        List<Organization> GetOrganization();

        void AddOrganization(Organization organization);

        void UpdateOrganization(Organization organization);

        void DeleteOrganization(int id);

        Organization fetchOrganizationById(int id);
    }
}
