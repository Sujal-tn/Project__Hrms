using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface.MasterDocuments.Documents
{
    public interface IEmployeeFileUpload
    {
        Task<List<User>> FetchAllUser();

        Task<List<UploadDocuments>> FetchAllDocuments();

        Task SaveFiles(EmployeeFileUploadViewModel model);

    }
}
