using Project_Hrms.Models;

namespace Project_Hrms.Interface
{
    public interface IEvent
    {
        Task AddNewEvent(Events e);

        Task <List<Events>> GetAllEvents();

        Task<Events> FindEventById(int id);

        Task UpdateEvent(Events e);

        Task DeleteEvent(int id);
    }
}
