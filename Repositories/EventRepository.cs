using Innoloft.Configurations;
using Innoloft.Data;
using Innoloft.DTOs.Requests;
using Innoloft.Models;
using Innoloft.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Innoloft.Repositories
{
    internal sealed class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EventRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Event>> GetAllAsync(PaginationFilter filter)
        {

            return await _dbContext.Events.Include(x => x.Users)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();
        }

        public async Task<Event> GetByIdAsync(string eventId) =>
            await _dbContext.Events.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == eventId);

        public async Task<Event> InsertAsync(Event eventInstance)
        {

            _dbContext.Events.Add(eventInstance);
            await _dbContext.SaveChangesAsync();
            return eventInstance;
        }

        public async Task UpdateAsync(EventDtoRequest eventDtoRequest)
        {
            Event eventInstance = await GetByIdAsync(eventDtoRequest.Id);
            eventInstance = eventInstance.UpdateEvent(eventDtoRequest);

            _dbContext.Events.Update(eventInstance);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Event eventInstance)
        {
            _dbContext.Events.Update(eventInstance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Event eventInstance)
        {

            _dbContext.Events.Remove(eventInstance);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddParticiptant(User user, Event eventInstance)
        {
            IEnumerable<User> participtants = eventInstance.AddParticiptant(user);
            eventInstance.Users = participtants;
            await UpdateAsync(eventInstance);
        }
    }
}
