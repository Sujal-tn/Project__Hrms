using Project_Hrms.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Interface
{
    public interface ITicket
    {
        Task<List<TicketViewModels>> GetAllTickets();
        Task<Ticket?> FindTicketById(int id);
        Task AddTicket(Ticket t);
        Task AssignTicket(int ticketId, int assignedToUserId, string? managerNote);
        Task StartWork(int ticketId);
        Task SubmitSolution(int ticketId, string solution);
        Task CloseTicket(int ticketId);
        Task ReopenTicket(int ticketId);
        Task DeleteTicket(int id);
        Task<List<User>> FetchUsers();
    }
}
