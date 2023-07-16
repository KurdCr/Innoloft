using Innoloft.Configurations;
using Innoloft.DTOs.Requests;
using Innoloft.Models;

namespace Innoloft.Repositories.Abstractions
{
    public interface IInvitationRepository

    {
        Task<IEnumerable<Invitation>> GetAllAsync(PaginationFilter filter);
        Task<Invitation> GetByIdAsync(string invitationId);
        Task<Invitation> InsertAsync(Invitation invitation);
        Task UpdateAsync(InvitationDtoRequest invitationDtoRequest);
        Task RemoveAsync(Invitation invitation);
    }
}
