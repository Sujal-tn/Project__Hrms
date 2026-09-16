using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface.EmployeeInterface
{
    public interface IDesignationService
    {
        void AddDesignation(Designation d);
        List<Designation> FetchDesignation();

        void DeleteDesignation(int id);

        Designation findDesignationById(int id);

        void UpdateDesignation(Designation r);

        List<Department> fetchDepartments();
    }
}
