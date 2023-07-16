using Innoloft.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using static Innoloft.Services.EventService;
using System.Security.Principal;
using Innoloft.Services.Abstractions;
using Innoloft.Repositories.Abstractions;
using Innoloft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Innoloft.DTOs.Requests;
using Innoloft.DTOs.Responses;
using Innoloft.Configurations;

namespace Innoloft.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public InvitationService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }



        public async Task<IEnumerable<InvitationDtoResponse>> GetAllAsync(PaginationFilter filter)
        {
            IEnumerable<Invitation> invitations = await _repositoryManager.InvitationRepository.GetAllAsync(filter);

            IEnumerable<InvitationDtoResponse> invitationDtoResponse = _mapper.Map<IEnumerable<Invitation>, IEnumerable<InvitationDtoResponse>>(invitations);
            return invitationDtoResponse;
        }

        public async Task<InvitationDtoResponse> GetByIdAsync(string invitationId)
        {
            Invitation invitation = await _repositoryManager.InvitationRepository.GetByIdAsync(invitationId);
            if (invitation is null) return null;
            InvitationDtoResponse invitationDtoResponse = _mapper.Map<Invitation, InvitationDtoResponse>(invitation);
            return invitationDtoResponse;
        }

        public async Task<InvitationDtoResponse> CreateAsync(InvitationDtoRequest invitationDtoRequest)
        {

            Invitation invitation = _mapper.Map<InvitationDtoRequest, Invitation>(invitationDtoRequest);
            invitation = await _repositoryManager.InvitationRepository.InsertAsync(invitation);
            InvitationDtoResponse invitationDtoResponse = _mapper.Map<Invitation, InvitationDtoResponse>(invitation);
            return invitationDtoResponse;
        }
        public async Task UpdateAsync(string invitationId, InvitationDtoRequest invitationDtoRequest)
        {
            var invitation = await _repositoryManager.InvitationRepository.GetByIdAsync(invitationId);
            if (invitation is null)
            {
                throw new Exception();
            }
            await _repositoryManager.InvitationRepository.UpdateAsync(invitationDtoRequest);
        }
        public async Task DeleteAsync(string invitationId)
        {
            Invitation invitation = await _repositoryManager.InvitationRepository.GetByIdAsync(invitationId);
            if (invitation is null)
            {
                throw new Exception();
            }
            await _repositoryManager.InvitationRepository.RemoveAsync(invitation);

        }






    }
}






