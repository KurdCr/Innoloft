using Innoloft.Configurations;
using Innoloft.Data;
using Innoloft.DTOs.Requests;
using Innoloft.Models;
using Innoloft.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Innoloft.Repositories
{
    internal sealed class InvitationRepository : IInvitationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InvitationRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Invitation>> GetAllAsync(PaginationFilter filter)
        {

            return await _dbContext.Invitations
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();
        }

        public async Task<Invitation> GetByIdAsync(string invitationId) =>
                await _dbContext.Invitations.FirstOrDefaultAsync(x => x.Id == invitationId);

        public async Task<Invitation> InsertAsync(Invitation invitation)
        {

            _dbContext.Invitations.Add(invitation);
            await _dbContext.SaveChangesAsync();
            return invitation;
        }
        public async Task UpdateAsync(InvitationDtoRequest invitationDtoRequest)
        {
            Invitation invitation = await GetByIdAsync(invitationDtoRequest.Id);
            invitation = invitation.UpdateInvitation(invitationDtoRequest);

            _dbContext.Invitations.Update(invitation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(Invitation invitation)
        {

            _dbContext.Invitations.Remove(invitation);
            await _dbContext.SaveChangesAsync();
        }
    }
}
