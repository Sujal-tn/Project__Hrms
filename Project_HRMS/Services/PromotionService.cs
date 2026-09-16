//using Microsoft.EntityFrameworkCore;
//using Project_Hrms.Data;
//using Project_Hrms.Interface;
//using Project_Hrms.Models;

//namespace Project_Hrms.Services
//{
//    public class PromotionService : IPromotion
//    {
//        private readonly ApplicationDbContext db;
//        public PromotionService(ApplicationDbContext db)
//        { 
//            this.db = db;
//        }
//        public async Task<List<User>> FetchUsers()
//        {
//            return await db.User.ToListAsync();
//        }
//        public async Task<List<Designation>> FetchDesignations()
//        { 
//            return await db.Designation.ToListAsync();
//        }
//    //    public async Task<List<User>> FetchUsers()
//    //    {
//    //        return await db.User.ToListAsync();
//    //    }
//    //    public async Task<List<Designation>> FetchDesignations()
//    //    { 
//    //        return await db.Designation.ToListAsync();
//    //    }
//    }
//}
