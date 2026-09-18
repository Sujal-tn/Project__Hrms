using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;
using Project_Hrms.Services;

namespace Project_Hrms.Services
{
    public class ResignationService : IResignation
    {
        private readonly ApplicationDbContext db;
        public ResignationService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<ResignationViewModels>> GetAllResignations()
        {
            var data = await (from r in db.Resignation join u in db.Users on r.UserId equals u.UserId
                              join d in db.Departments on r.DepartmentId equals d.DepartmentId into deptGroup
                              from d in deptGroup.DefaultIfEmpty()
                              select new ResignationViewModels
                              {
                                  ResignationId = r.ResignationId,
                                  UserId = r.UserId,
                                  EmployeeName = (u.FirstName + " " + u.LastName).Trim(),
                                  ProfilePicture = u.ProfilePicture,
                                  DepartmentId = r.DepartmentId,
                                  DepartmentName = d != null ? d.DepartmentName : "N/A",
                                  NoticeDate = r.NoticeDate,
                                  ResignDate = r.ResignDate,
                                  NoticeDateFormat = r.NoticeDate.ToString("yyyy-MM-dd"),
                                  ResignDateFormat = r.ResignDate.ToString("yyyy-MM-dd"),
                                  Reason = r.Reason
                              }).ToListAsync();
            return data;
        }
        public async Task<Resignation?> FindResignationById(int id)
        {
            var data = await db.Resignation.FindAsync(id);
            return data;
        }
        public async Task AddResignation(Resignation r)
        {
            await db.Resignation.AddAsync(r);
            await db.SaveChangesAsync();
        }
        public async Task UpdateResignation(Resignation r)
        {
            db.Resignation.Update(r);
            await db.SaveChangesAsync();
        }
        public async Task DeleteResignation(int id)
        {
            var data = await db.Resignation.FindAsync(id);
            if (data != null)
            {
                db.Resignation.Remove(data);
                await db.SaveChangesAsync();
            }
        }
        public async Task<List<User>> FetchUsers()
        {
            return await db.Users.ToListAsync();
        }
        public async Task<List<Department>> FetchDepartments()
        {
            return await db.Departments.ToListAsync();
        }
    }
}
