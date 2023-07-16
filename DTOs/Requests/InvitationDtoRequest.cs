using Innoloft.Models;
using System.ComponentModel.DataAnnotations;

namespace Innoloft.DTOs.Requests
{
    public class InvitationDtoRequest
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public string UserId { get; set; }
        public InvitationStatus Status { get; set; }
    }
}