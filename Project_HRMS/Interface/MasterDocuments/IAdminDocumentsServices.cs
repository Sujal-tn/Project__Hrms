using Project_Hrms.Models;

namespace Project_Hrms.Interface.MasterDocuments
{
    public interface IAdminDocumentsServices
    {
        public Task<List<AdminAddDocumentsName>> FetchAll();

        public Task<AdminAddDocumentsName?> FindByID(int id);

        public Task AddAdminDocuments(AdminAddDocumentsName t);

        public Task UpdateAdminDocuments(AdminAddDocumentsName t);

        public Task DeleteAdminDocuments(int id);

        
    }
}
