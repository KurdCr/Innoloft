using Innoloft.DTOs;
using Innoloft.DTOs.Requests;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Innoloft.Models
{
    public class Event
    {

        public Event()
        {
            Users = new List<User>();
        }
        public Event(string id, string Title, string Description, string Location, DateTime StartDate, DateTime EndDate)
        { //used for unit testing
            this.Id = id;
            this.Title = Title;
            this.Description = Description;
            this.Location = Location;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            Users = new List<User>();
        }


        public string Id { get; private set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, ErrorMessage = "Name can't be longer than 30 characters")]
        public string Title { get; private set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(300, ErrorMessage = "Description can't be longer than 300 characters")]
        public string Description { get; private set; }
        public string Location { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string? EventOwnerId { get; private set; }
        public IEnumerable<User>? Users { get; set; }

        internal IEnumerable<User> AddParticiptant(User user)
        {
            // Do some checks before letting users participate
            if (user.Id != EventOwnerId)
            {
                Users = Users.Append(user);
            }
            Users.Append(user);
            return Users;

        }
        public Event UpdateEvent(EventDtoRequest eventDtoRequest)
        {
            this.Id = eventDtoRequest.Id;
            this.Title = eventDtoRequest.Title;
            this.Description = eventDtoRequest.Description;
            this.Location = eventDtoRequest.Location;
            this.StartDate = eventDtoRequest.StartDate;
            this.EndDate = eventDtoRequest.EndDate;

            return this;
        }
    }

}



