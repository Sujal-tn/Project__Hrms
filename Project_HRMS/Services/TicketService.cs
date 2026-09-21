using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Hrms.Services

{
    public class TicketService : ITicket
    {
        private readonly ApplicationDbContext db;
        public TicketService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<List<TicketViewModels>> GetAllTickets()
        {
            var data = await (from t in db.Ticket join r in db.Users on t.RaisedByUserId equals r.UserId into raisedJoin
                              from r in raisedJoin.DefaultIfEmpty() join a in db.Users on t.AssignedToUserId equals a.UserId into assignedJoin
                              from a in assignedJoin.DefaultIfEmpty()
                              select new TicketViewModels
                              {
                                  TicketId = t.TicketId,
                                  TicketNo = t.TicketNo,
                                  Subject = t.Subject,
                                  Description = t.Description,
                                  Priority = t.Priority,
                                  Status = t.Status,
                                  RaisedByUserId = t.RaisedByUserId,
                                  RaisedByName = r != null ? r.FirstName + " " + r.LastName : "N/A",
                                  AssignedToUserId = t.AssignedToUserId,
                                  AssignedToName = a != null ? a.FirstName + " " + a.LastName : "Unassigned",
                                  ManagerNote = t.ManagerNote,
                                  Solution = t.Solution,
                                  CreatedDate = t.CreatedDate,
                                  FormattedDate = t.CreatedDate.ToString("dd MMM yyyy")
                              }).OrderByDescending(x => x.TicketId).ToListAsync();
            return data;
        }
        public async Task<Ticket> FindTicketById(int id)
        {
            var data = await db.Ticket.FindAsync(id);
            return data;
        }
        public async Task AddTicket(Ticket t)
        {
            var lastTicket = await db.Ticket.OrderByDescending(x => x.TicketId).FirstOrDefaultAsync();
            int nextNumber = lastTicket != null ? lastTicket.TicketId + 10001 : 10001;
            t.TicketNo = $"TKT-{nextNumber}";
            t.Status = "Open";
            t.CreatedDate = DateTime.Now;

            await db.Ticket.AddAsync(t);
            await db.SaveChangesAsync();
        }
        public async Task AssignTicket(int ticketId, int assignedToUserId, string? managerNote)
        {
            var ticket = await db.Ticket.FindAsync(ticketId);
            if (ticket != null)
            {
                ticket.AssignedToUserId = assignedToUserId;
                ticket.ManagerNote = managerNote;
                ticket.Status = "Assigned";
                await db.SaveChangesAsync();
            }
        }
        public async Task StartWork(int ticketId)
        {
            var ticket = await db.Ticket.FindAsync(ticketId);
            if (ticket != null)
            {
                ticket.Status = "In Progress";
                await db.SaveChangesAsync();
            }
        }
        public async Task SubmitSolution(int ticketId, string solution)
        {
            var ticket = await db.Ticket.FindAsync(ticketId);
            if (ticket != null)
            {
                ticket.Solution = solution;
                ticket.Status = "Resolved";
                ticket.ResolvedDate = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }
        public async Task CloseTicket(int ticketId)
        {
            var ticket = await db.Ticket.FindAsync(ticketId);
            if (ticket != null)
            {
                ticket.Status = "Closed";
                ticket.ClosedDate = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }
        public async Task ReopenTicket(int ticketId)
        {
            var ticket = await db.Ticket.FindAsync(ticketId);
            if (ticket != null)
            {
                ticket.Status = "Reopened";
                await db.SaveChangesAsync();
            }
        }
        public async Task DeleteTicket(int id)
        {
            var data = await db.Ticket.FindAsync(id);
            if (data != null)
            {
                db.Ticket.Remove(data);
                await db.SaveChangesAsync();
            }
        }
        public async Task<List<User>> FetchUsers()
        {
            return await db.Users.ToListAsync();
        }
    }
}
