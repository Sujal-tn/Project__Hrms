using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface.MasterDocuments.Documents
{
    public interface IAdminFileUpload
    {
        Task<List<User>> FetchAllUsers();

        Task<List<AdminAddDocumentsName>> FetchAllDocumentNames();

        Task SaveFiles(AdminFileUploadViewModel model);
    }
}
