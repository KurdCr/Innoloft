using AutoMapper;
using Innoloft.Configurations;
using Innoloft.Data;
using Innoloft.DTOs.Requests;
using Innoloft.DTOs.Responses;
using Innoloft.Models;
using Innoloft.Repositories.Abstractions;
using Innoloft.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Security.Policy;

namespace Innoloft.Services
{
    public class EventService : IEventService
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EventService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }



        public async Task<IEnumerable<EventDtoResponse>> GetAllAsync(PaginationFilter filter)
        {
            IEnumerable<Event> events = await _repositoryManager.EventRepository.GetAllAsync(filter);

            IEnumerable<EventDtoResponse> eventDtoResponses = _mapper.Map<IEnumerable<Event>, IEnumerable<EventDtoResponse>>(events);
            return eventDtoResponses;
        }

        public async Task<EventDtoResponse> GetByIdAsync(string eventId)
        {
            Event eventInstance = await _repositoryManager.EventRepository.GetByIdAsync(eventId);
            if (eventInstance is null) return null;
            EventDtoResponse eventDtoResponse = _mapper.Map<Event, EventDtoResponse>(eventInstance);
            return eventDtoResponse;
        }

        public async Task<EventDtoResponse> CreateAsync(EventDtoRequest eventDtoRequest)
        {

            Event eventInstance = _mapper.Map<EventDtoRequest, Event>(eventDtoRequest);
            eventInstance = await _repositoryManager.EventRepository.InsertAsync(eventInstance);
            EventDtoResponse eventDtoResponse = _mapper.Map<Event, EventDtoResponse>(eventInstance);
            return eventDtoResponse;
        }

        public async Task UpdateAsync(string eventId, EventDtoRequest eventDtoRequest)
        {
            var eventInstance = await _repositoryManager.EventRepository.GetByIdAsync(eventId);
            if (eventInstance is null)
            {
                throw new Exception();
            }
            await _repositoryManager.EventRepository.UpdateAsync(eventDtoRequest);
        }
        public async Task DeleteAsync(string eventId)
        {
            Event eventInstance = await _repositoryManager.EventRepository.GetByIdAsync(eventId);
            if (eventInstance is null)
            {
                throw new Exception();
            }
            await _repositoryManager.EventRepository.RemoveAsync(eventInstance);

        }


        public async Task<List<EventParticipantResponse>> GetParticipants(string eventId)
        {
            Event eventInstance = await _repositoryManager.EventRepository.GetByIdAsync(eventId);
            List<EventParticipantResponse> eventParticipantResponses = new List<EventParticipantResponse>();
            EventParticipantResponse eventParticipantResponse = new EventParticipantResponse();
            if (eventInstance.Users.Count() > 0)
                foreach (User item in eventInstance.Users)
                {

                    eventParticipantResponse.UserId = item.Id;
                    eventParticipantResponse.Name = item.Name;
                    eventParticipantResponse.Email = item.Email;
                    eventParticipantResponse.EventId = eventId;

                    eventParticipantResponses.Add(
                     eventParticipantResponse
                    );
                }
            return eventParticipantResponses;
        }
        public async Task Participate(EventParticipantRequest eventParticiptantRequest)
        {
            User user = await _repositoryManager.UserRepository.GetByIdAsync(eventParticiptantRequest.UserId);
            Event eventInstance = await _repositoryManager.EventRepository.GetByIdAsync(eventParticiptantRequest.EventId);
            await _repositoryManager.EventRepository.AddParticiptant(user, eventInstance);

        }





    }
}

