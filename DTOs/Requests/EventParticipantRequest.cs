using Innoloft.Models;
using System.ComponentModel.DataAnnotations;

namespace Innoloft.DTOs.Requests
{
    public class EventParticipantRequest
    {
        public string UserId { get; set; }
        public string EventId { get; set; }
    }
}