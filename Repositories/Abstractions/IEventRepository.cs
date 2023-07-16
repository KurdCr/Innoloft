using Innoloft.Configurations;
using Innoloft.DTOs.Requests;
using Innoloft.Models;

namespace Innoloft.Repositories.Abstractions
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync(PaginationFilter filter);
        Task<Event> GetByIdAsync(string eventId);
        Task<Event> InsertAsync(Event eventInstance);
        Task UpdateAsync(EventDtoRequest eventDtoRequest);
        Task RemoveAsync(Event eventInstance);
        Task AddParticiptant(User user, Event eventInstance);
    }
}
