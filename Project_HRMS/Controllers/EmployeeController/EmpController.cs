using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Controllers.EmployeeController
{
    public class EmpController : Controller
    {
       
        IEmpService es;
        IBankInformationService bs;
        IFamilyService fs;
        IEducationService  edu;

        IExperienceService ex;
        public EmpController(IEmpService es, IBankInformationService bs, IFamilyService fs, IEducationService edu, IExperienceService ex)
        {
            this.es = es;
            this.bs = bs;
            this.fs = fs;
            this.edu = edu;
            this.ex = ex;
        }
        public IActionResult Index()
        {

            var users = es.FetchEmp();
            var TotalUsers = users.Count();
            var ActiveUsers = users.Count(u => u.Status == "Active");
            var InActiveUsers = users.Count(u => u.Status != "Active");
            var NewJoiner = users.Count(u => DateTime.Parse(u.DateOfJoining).Month == DateTime.Now.Month &&
                  DateTime.Parse(u.DateOfJoining).Year == DateTime.Now.Year
            );
            ViewBag.TUser = TotalUsers;
            ViewBag.Auser = ActiveUsers;
            ViewBag.InUser = InActiveUsers;
            ViewBag.NewJ = NewJoiner;
            return View(users);
        }

        public async Task<IActionResult> AddEmp(int? roleId)
        {
            var rs = es.fetchRole();
            ViewBag.Role = rs;
            var ds = es.fetchDepartments();
            ViewBag.Depart = ds;
            var des = es.fetchDesignation();
            ViewBag.Desig = des;
            var ma = await es.FetchManagersAsync();
            ViewBag.Man = ma;
            if (roleId != null)
            {
                ViewBag.rid = roleId.Value;

                ViewBag.rname= es.GetRoleName(roleId.Value);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddEmp(UserView u)
        {
            es.AddEmp(u);
            TempData["msg"] = "Added Successfully";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteEmp(int id)
        {
            es.DeleteEmp(id);
            TempData["delmsg"] = "Added Successfully";
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> EditEmp(int id)
        {
            var emp = es.findEmpById(id);

            if (emp == null)
            {
                return NotFound();
            }

            var rs = es.fetchRole();
            ViewBag.Role = rs;

            var ds = es.fetchDepartments();
            ViewBag.Depart = ds;

            var des = es.fetchDesignation();
            ViewBag.Desig = des;

            var ma = await es.FetchManagersAsync();
            ViewBag.Man = ma;
            return View(emp);
        }
        [HttpPost]
        public IActionResult EditEmp(User u)
        {
            es.UpdateEmp(u);
            TempData["updmsg"] = "Added Successfully";
            return RedirectToAction("Index");
        }


        public IActionResult EmpGrid()
        {
            var users = es.FetchEmp();
            var TotalUsers = users.Count();
            var ActiveUsers = users.Count(u => u.Status == "Active");
            var InActiveUsers = users.Count(u => u.Status != "Active");
            var NewJoiner = users.Count(u => DateTime.Parse(u.DateOfJoining).Month == DateTime.Now.Month &&
                  DateTime.Parse(u.DateOfJoining).Year == DateTime.Now.Year
            );
            ViewBag.TUser = TotalUsers;
            ViewBag.Auser = ActiveUsers;
            ViewBag.InUser = InActiveUsers;
            ViewBag.NewJ = NewJoiner;
           
            var cards = es.GetEmployeeCardData();
            return View(cards);
        }


        public IActionResult EmpDetails()
        {
            var id = HttpContext.Session.GetString("UserId");
            
            if(id==null)
            {
                return RedirectToAction("Login", "Auth");
            }
            int uid = int.Parse(id);
           var emps = es.findEmpById(uid);

            return View(emps);
        }

        public IActionResult AddBankInformation(int userId)
        {
            var bank = bs.GetBankInfo(userId);
            return View(bank);
        }

        [HttpPost]
        public IActionResult AddBankInformation(BankInformation bank)
        {
            bs.AddBankInfo(bank);
            return RedirectToAction("EmpDetail");
        }

        [HttpPost]
        public IActionResult EditBankInformation(BankInformation bank)
        {
            bs.UpdateBankInfo(bank);
            return RedirectToAction("EmpDetail");
        }

        public IActionResult DeleteBankInformation(int id)
        {
            bs.DeleteBankInfo(id);
            return RedirectToAction("EmpDetail");
        }

    }
}
