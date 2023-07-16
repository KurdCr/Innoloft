namespace Innoloft.Services.Abstractions
{
    public interface IServiceManager
    {
        IEventService EventService { get; }
        IInvitationService InvitationService { get; }
        IUserService UserService { get; }
    }
}
