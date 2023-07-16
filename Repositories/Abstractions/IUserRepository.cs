using Innoloft.Configurations;
using Innoloft.DTOs.Requests;
using Innoloft.Models;

namespace Innoloft.Repositories.Abstractions
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(PaginationFilter filter);
        Task<User> GetByIdAsync(string userId);
        Task<User> InsertAsync(User user);
        Task UpdateAsync(UserDtoRequest userDtoRequest);
        Task RemoveAsync(User user);
    }
}
