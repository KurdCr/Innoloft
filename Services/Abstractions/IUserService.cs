using Innoloft.Configurations;
using Innoloft.DTOs;
using Innoloft.DTOs.Requests;
using Innoloft.DTOs.Responses;

namespace Innoloft.Services.Abstractions
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDtoResponse>> GetAllAsync(PaginationFilter filter);

        public Task<UserDtoResponse> GetByIdAsync(string eventId);

        public Task<UserDtoResponse> CreateAsync(UserDtoRequest userDtoRequest);
        public Task UpdateAsync(string eventId, UserDtoRequest userDtoRequest);

        public Task DeleteAsync(string eventId);
    }
}
