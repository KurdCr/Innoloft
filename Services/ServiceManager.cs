using AutoMapper;
using Innoloft.Data;
using Innoloft.DTOs;
using Innoloft.Models;
using Innoloft.Models;
using Innoloft.Repositories;
using Innoloft.Repositories.Abstractions;
using Innoloft.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace Innoloft.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEventService> _eventService;
        private readonly Lazy<IInvitationService> _invitationsService;
        private readonly Lazy<IUserService> _userService;
        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _eventService = new Lazy<IEventService>(() => new EventService(repositoryManager, mapper));
            _invitationsService = new Lazy<IInvitationService>(() => new InvitationService(repositoryManager, mapper));
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper));
        }
        public IEventService EventService => _eventService.Value;
        public IInvitationService InvitationService => _invitationsService.Value;
        public IUserService UserService => _userService.Value;

    }
}
