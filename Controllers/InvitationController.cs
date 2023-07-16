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
    [Route("api/invitations")]
    public class InvitationController : ControllerBase
    {


        private IServiceManager _serviceManager;
        private readonly ILogger<InvitationController> _logger;
        private const string invitationsCacheKey = "invitationsList";
        private readonly IMemoryCache _cache;

        public InvitationController(ILogger<InvitationController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024 * 64,
            }); ;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvitationDtoResponse>>> GetInvitations([FromQuery] PaginationFilter filter)
        {
            IEnumerable<InvitationDtoResponse> invitationDtoResponses;
            if (_cache.TryGetValue(invitationsCacheKey, out invitationDtoResponses))
            {
                _logger.Log(LogLevel.Information, "Invitation list found in cache.");
            }
            else
            {
                invitationDtoResponses = await _serviceManager.InvitationService.GetAllAsync(filter);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(1800))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);
                _cache.Set(invitationsCacheKey, invitationDtoResponses, cacheEntryOptions);
            }
            return Ok(invitationDtoResponses);
        }
        [HttpGet("{invitationId}")]
        public async Task<ActionResult<InvitationDtoResponse>> GetInvitation(string invitationId)
        {
            if (await _serviceManager.InvitationService.GetByIdAsync(invitationId) == null)
            {
                return NotFound(ModelState);
            }
            InvitationDtoResponse invitationDtoResponse = await _serviceManager.InvitationService.GetByIdAsync(invitationId);
            return Ok(invitationDtoResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvitation([FromBody] InvitationDtoRequest invitationDtoRequest)
        {
            if (await _serviceManager.EventService.GetByIdAsync(invitationDtoRequest.EventId) == null)
            {
                return NotFound(ModelState);
            }
            if (await _serviceManager.UserService.GetByIdAsync(invitationDtoRequest.UserId) == null)
            {
                return NotFound(ModelState);
            }
            InvitationDtoResponse invitationDtoResponse = await _serviceManager.InvitationService.CreateAsync(invitationDtoRequest);
            _cache.Remove(invitationsCacheKey);
            return Ok(invitationDtoResponse);
        }

        [HttpPut("{invitationId}")]
        public async Task<IActionResult> UpdateInvitation(string invitationId, [FromBody] InvitationDtoRequest invitationDtoRequest)
        {
            if (await _serviceManager.InvitationService.GetByIdAsync(invitationId) == null)
            {
                return NotFound(ModelState);
            }
            await _serviceManager.InvitationService.UpdateAsync(invitationId, invitationDtoRequest);
            _cache.Remove(invitationsCacheKey);
            return NoContent();
        }
        [HttpDelete("{invitationId}")]
        public async Task<IActionResult> DeleteInvitation(string invitationId)
        {
            await _serviceManager.InvitationService.DeleteAsync(invitationId);
            _cache.Remove(invitationsCacheKey);
            return NoContent();
        }

    }

}












