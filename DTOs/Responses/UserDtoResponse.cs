using Innoloft.Models;
using System.ComponentModel.DataAnnotations;

namespace Innoloft.DTOs.Responses
{
    public class UserDtoResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public IEnumerable<Event>? Events { get; set; }
        public IEnumerable<Invitation>? Invitations { get; set; }
    }
}