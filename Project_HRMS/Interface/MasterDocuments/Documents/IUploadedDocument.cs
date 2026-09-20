using Project_Hrms.Models;

namespace Project_Hrms.Interface.MasterDocuments.Documents
{
    public interface IUploadedDocument
    {
        Task<List<FileUploads>> FetchAdminFiles();
        Task<List<FileUploads>> FetchEmployeeFiles();

        Task<FileUploads?> FindById(int id);

        Task DeleteFile(int id);
    }
}
