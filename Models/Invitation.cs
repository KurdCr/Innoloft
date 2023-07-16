using Innoloft.DTOs.Requests;
using Innoloft.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Innoloft
{
    public class Invitation
    {

        public string Id { get; private set; }
        public string EventId { get; private set; }
        public Event Event { get; private set; }
        public string UserId { get; private set; }
        public User User { get; private set; }
        public InvitationStatus Status { get; private set; }

        public Invitation UpdateInvitation(InvitationDtoRequest invitationDtoRequest)
        {
            this.Status = invitationDtoRequest.Status;

            return this;
        }
    }

    public enum InvitationStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = -1
    }


}



