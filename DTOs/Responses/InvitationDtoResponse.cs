using Innoloft.Models;
using System.ComponentModel.DataAnnotations;

namespace Innoloft.DTOs.Responses
{
    public class InvitationDtoResponse
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public string UserId { get; set; }
        public InvitationStatus Status { get; set; }
    }
}