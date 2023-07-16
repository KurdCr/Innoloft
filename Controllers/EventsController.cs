using AutoMapper;
using Innoloft;
using Innoloft.Configurations;
using Innoloft.DTOs.Requests;
using Innoloft.DTOs.Responses;
using Innoloft.Models;
using Innoloft.Services;
using Innoloft.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace Innoloft.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private IServiceManager _serviceManager;
        private const string eventsListCacheKey = "eventsList";
        private const string participantsListCacheKey = "participantsList";
        private readonly IMemoryCache _cache;


        public EventsController(ILogger<EventsController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024 * 64,
            }); ;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDtoResponse>>> GetEvents([FromQuery] PaginationFilter filter)
        {
            IEnumerable<EventDtoResponse> eventDtoResponse;
            if (_cache.TryGetValue(eventsListCacheKey, out eventDtoResponse))
            {
                _logger.Log(LogLevel.Information, "Event list found in cache.");
            }
            else
            {
                eventDtoResponse = await _serviceManager.EventService.GetAllAsync(filter);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(1800))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);
                _cache.Set(eventsListCacheKey, eventDtoResponse, cacheEntryOptions);
            }
            return Ok(eventDtoResponse);
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<EventDtoResponse>> GetEvent(string eventId)
        {
            if (await _serviceManager.EventService.GetByIdAsync(eventId) == null)
            {
                return NotFound(ModelState);
            }
            EventDtoResponse eventDtoResponse = await _serviceManager.EventService.GetByIdAsync(eventId);
            return Ok(eventDtoResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventDtoRequest eventDtoRequest)
        {
            if (await _serviceManager.UserService.GetByIdAsync(eventDtoRequest.EventOwnerId) == null)
            {
                return NotFound(ModelState);
            }
            EventDtoResponse eventDtoResponse = await _serviceManager.EventService.CreateAsync(eventDtoRequest);
            _cache.Remove(eventsListCacheKey);
            return Ok(eventDtoResponse);
        }

        [HttpPut("{eventId}")]

        public async Task<IActionResult> UpdateEvent(string eventId, [FromBody] EventDtoRequest eventDtoRequest)
        {
            if (await _serviceManager.EventService.GetByIdAsync(eventId) == null)
            {
                return NotFound(ModelState);
            }
            await _serviceManager.EventService.UpdateAsync(eventId, eventDtoRequest);
            _cache.Remove(eventsListCacheKey);
            return NoContent();
        }
        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent(string eventId)
        {
            if (await _serviceManager.EventService.GetByIdAsync(eventId) == null)
            {
                return NotFound(ModelState);
            }
            await _serviceManager.EventService.DeleteAsync(eventId);
            _cache.Remove(eventsListCacheKey);
            return NoContent();
        }

        [HttpGet("{eventId}/participants")]
        public async Task<ActionResult<EventParticipantResponse>> GetParticipants(string eventId)
        {
            List<EventParticipantResponse> eventParticipantResponses;
            if (_cache.TryGetValue(participantsListCacheKey, out eventParticipantResponses))
            {
                _logger.Log(LogLevel.Information, "Participant list found in cache.");
            }
            else
            {
                eventParticipantResponses = await _serviceManager.EventService.GetParticipants(eventId);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(1800))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);
                _cache.Set(participantsListCacheKey, eventParticipantResponses, cacheEntryOptions);
            }



            return Ok(eventParticipantResponses);
        }
        [HttpPost]
        [Route("{eventId}/participants")]
        public async Task<IActionResult> Participate(string eventId, [FromBody] EventParticipantRequest eventParticiptantRequest)
        {
            if (await _serviceManager.EventService.GetByIdAsync(eventParticiptantRequest.EventId) == null)
            {
                return NotFound(ModelState);
            }
            if (await _serviceManager.UserService.GetByIdAsync(eventParticiptantRequest.UserId) == null)
            {
                return NotFound(ModelState);
            }
            await _serviceManager.EventService.Participate(eventParticiptantRequest);
            _cache.Remove(eventsListCacheKey);
            return Ok();
        }


    }

}












