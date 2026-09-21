using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Hrms.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicket tic;
        public TicketController(ITicket t)
        {
            this.tic = t;
        }
        private int GetCurrentUserId()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (int.TryParse(userIdStr, out int id))
            {
                return id;
            }
            return 0;
        }
        private string GetCurrentUserRole()
        {
            return HttpContext.Session.GetString("RoleName") ?? "Employee";
        }
        public async Task<IActionResult> Index()
        {
            int currentUserId = GetCurrentUserId();
            string roleName = GetCurrentUserRole();

            if (currentUserId == 0)
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.CurrentUserId = currentUserId;
            ViewBag.UserRole = roleName;
            ViewBag.Users = await tic.FetchUsers();

            var allTickets = await tic.GetAllTickets();

            if (roleName == "Manager")
            {
                return View(allTickets);
            }
            else
            {
                var employeeTickets = allTickets
                    .Where(t => t.RaisedByUserId == currentUserId || t.AssignedToUserId == currentUserId)
                    .ToList();

                return View(employeeTickets);
            }
        }
        public async Task<IActionResult> AddTicket()
        {
            ViewBag.Users = await tic.FetchUsers();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTicket(Ticket t)
        {
            int currentUserId = GetCurrentUserId();
            if (currentUserId == 0)
            {
                return RedirectToAction("Login", "Auth");
            }

            t.RaisedByUserId = currentUserId;

            await tic.AddTicket(t);
            TempData["addmssg"] = "Ticket raised successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AssignTicket(int ticketId, int assignedToUserId, string? managerNote)
        {
            await tic.AssignTicket(ticketId, assignedToUserId, managerNote);
            TempData["addmssg"] = "Ticket assigned to employee!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> StartWork(int id)
        {
            await tic.StartWork(id);
            TempData["addmssg"] = "Ticket marked as In Progress!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitSolution(int ticketId, string solution)
        {
            await tic.SubmitSolution(ticketId, solution);
            TempData["addmssg"] = "Solution submitted! Ticket status is Resolved.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CloseTicket(int id)
        {
            await tic.CloseTicket(id);
            TempData["addmssg"] = "Ticket closed successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ReopenTicket(int id)
        {
            await tic.ReopenTicket(id);
            TempData["addmssg"] = "Ticket reopened!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteTicket(int id)
        {
            await tic.DeleteTicket(id);
            TempData["addmssg"] = "Ticket deleted!";
            return RedirectToAction("Index");
        }
    }
}
