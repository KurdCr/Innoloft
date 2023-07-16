using Innoloft.Services.Abstractions;

namespace Innoloft.Repositories.Abstractions
{
    public interface IRepositoryManager
    {
        IEventRepository EventRepository { get; }
        IInvitationRepository InvitationRepository { get; }
        IUserRepository UserRepository { get; }
    }

}
