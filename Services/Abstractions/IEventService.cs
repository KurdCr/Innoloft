using Innoloft.Configurations;
using Innoloft.DTOs.Requests;
using Innoloft.DTOs.Responses;

namespace Innoloft.Services.Abstractions
{
    public interface IEventService
    {
        public Task<IEnumerable<EventDtoResponse>> GetAllAsync(PaginationFilter filter);

        public Task<EventDtoResponse> GetByIdAsync(string eventId);

        public Task<EventDtoResponse> CreateAsync(EventDtoRequest eventDtoRequest);
        public Task UpdateAsync(string eventId, EventDtoRequest eventDtoRequest);

        public Task DeleteAsync(string eventId);

        Task<List<EventParticipantResponse>> GetParticipants(string eventId);
        public Task Participate(EventParticipantRequest eventParticiptantRequest);
      
    }
}
