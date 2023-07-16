using Innoloft.Configurations;
using Innoloft.Data;
using Innoloft.DTOs.Requests;
using Innoloft.Models;
using Innoloft.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Innoloft.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<User>> GetAllAsync(PaginationFilter filter)
        {

            return await _dbContext.Users
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();
        }

        public async Task<User> GetByIdAsync(string userId) =>
            await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

        public async Task<User> InsertAsync(User user)
        {
            user = user.CreateUserUsingFakeJson(user);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(UserDtoRequest userDtoRequest)
        {
            User user = await GetByIdAsync(userDtoRequest.Id);
            user = user.UpdateUser(userDtoRequest);

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

    }
}
