using Project_Hrms.Models;

namespace Project_Hrms.Interface.MasterDocuments
{
    public interface IEmployeeDocumentsServices
    {
        public Task<List<EmployeeAddDocumentsName>> FetchAll();

        public Task<EmployeeAddDocumentsName?> FindByID(int id);

        public Task AddEmployeeDocuments(EmployeeAddDocumentsName t);

        public Task UpdateEmployeeDocuments(EmployeeAddDocumentsName t);

        public Task DeleteEmployeeDocuments(int id);

    }
}
