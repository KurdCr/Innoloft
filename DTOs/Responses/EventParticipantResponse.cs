using Innoloft.Models;
using System.ComponentModel.DataAnnotations;

namespace Innoloft.DTOs.Requests
{
    public class EventParticipantResponse
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EventId { get; set; }
    }
}