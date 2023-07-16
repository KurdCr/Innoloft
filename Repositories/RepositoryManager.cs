
using Innoloft.Services;
using Innoloft.Repositories.Abstractions;
using Innoloft.Data;

namespace Innoloft.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {


        private readonly Lazy<IEventRepository> _eventRepository;
        private readonly Lazy<IInvitationRepository> _invitationRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        public RepositoryManager(ApplicationDbContext dbContex)
        {
            _eventRepository = new Lazy<IEventRepository>(() => new EventRepository(dbContex));
            _invitationRepository = new Lazy<IInvitationRepository>(() => new InvitationRepository(dbContex));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(dbContex));
        }
        public IEventRepository EventRepository => _eventRepository.Value;
        public IInvitationRepository InvitationRepository => _invitationRepository.Value;
        public IUserRepository UserRepository => _userRepository.Value;



    }
}
