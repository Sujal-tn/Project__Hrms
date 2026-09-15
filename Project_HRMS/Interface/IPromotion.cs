using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface IPromotion
    {
        Task<List<PromotionViewModels>> GetAllPromotions();
        Task<Promotion?> FindPromotionById(int id);
        Task AddPromotion(Promotion p);
        Task UpdatePromotion(Promotion p);
        Task DeletePromotion(int id);
        Task<List<User>> FetchUsers();
        Task<List<Designation>> FetchDesignations();

    }
}
