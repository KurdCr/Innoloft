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
    [Route("api/users")]
    public class UserController : ControllerBase
    {


        private IServiceManager _serviceManager;
        private readonly ILogger<UserController> _logger;
        private const string usersCacheKey = "usersList";
        private readonly IMemoryCache _cache;

        public UserController(ILogger<UserController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024 * 64,
            }); ;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDtoResponse>>> GetUsers([FromQuery] PaginationFilter filter)
        {

            IEnumerable<UserDtoResponse> userDtoResponses;
            if (_cache.TryGetValue(usersCacheKey, out userDtoResponses))
            {
                _logger.Log(LogLevel.Information, "User list found in cache.");
            }
            else
            {
                userDtoResponses = await _serviceManager.UserService.GetAllAsync(filter);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(1800))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);
                _cache.Set(usersCacheKey, userDtoResponses, cacheEntryOptions);
            }
            return Ok(userDtoResponses);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDtoResponse>> GetUser(string userId)
        {
            if (await _serviceManager.UserService.GetByIdAsync(userId) == null)
            {
                return NotFound(ModelState);
            }
            UserDtoResponse UserDtoResponse = await _serviceManager.UserService.GetByIdAsync(userId);
            return Ok(UserDtoResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDtoRequest userDtoRequest)
        {
            UserDtoResponse UserDtoResponse = await _serviceManager.UserService.CreateAsync(userDtoRequest);
            _cache.Remove(usersCacheKey);
            return Ok(UserDtoResponse);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserDtoRequest userDtoRequest)
        {
            if (await _serviceManager.UserService.GetByIdAsync(userId) == null)
            {
                return NotFound(ModelState);
            }
            await _serviceManager.UserService.UpdateAsync(userId, userDtoRequest);
            _cache.Remove(usersCacheKey);
            return NoContent();
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _serviceManager.UserService.DeleteAsync(userId);
            _cache.Remove(usersCacheKey);
            return NoContent();
        }

    }

}












