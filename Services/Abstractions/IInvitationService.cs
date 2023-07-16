using Innoloft.Configurations;
using Innoloft.DTOs.Requests;
using Innoloft.DTOs.Responses;

namespace Innoloft.Services.Abstractions
{
    public interface IInvitationService
    {
        public Task<IEnumerable<InvitationDtoResponse>> GetAllAsync(PaginationFilter filter);

        public Task<InvitationDtoResponse> GetByIdAsync(string invitationId);

        public Task<InvitationDtoResponse> CreateAsync(InvitationDtoRequest invitationDtoRequest);
        public Task UpdateAsync(string invitationId, InvitationDtoRequest invitationDtoRequest);

        public Task DeleteAsync(string invitationId);
    }
}
