using Innoloft.DTOs.Requests;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Innoloft.Models
{
    public class User
    {
        public User()
        {
            Events = new List<Event>();
            Invitations = new List<Invitation>();
        }
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, ErrorMessage = "Name can't be longer than 30 characters")]
        public string Name { get; private set; }
        public string? Email { get; private set; }
        public string? Phone { get; private set; }
        public string? Website { get; private set; }
        public IEnumerable<Event>? Events { get; set; }
        public IEnumerable<Invitation>? Invitations { get; set; }

        internal User CreateUserUsingFakeJson(User user)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string json = (new WebClient()).DownloadString($"https://jsonplaceholder.typicode.com/users/{user.Id}");
            Console.WriteLine(json);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var dynamicobject = JsonConvert.DeserializeObject<dynamic>(json);

            this.Id = dynamicobject.id;
            this.Name = dynamicobject.name;
            this.Email = dynamicobject.email;
            this.Phone = dynamicobject.phone;
            this.Website = dynamicobject.website;
            return user;
        }

        public User UpdateUser(UserDtoRequest userDtoRequest)
        {
            this.Id = userDtoRequest.Id;
            this.Name = userDtoRequest.Name;
            this.Email = userDtoRequest.Email;
            this.Phone = userDtoRequest.Phone;
            this.Website = userDtoRequest.Website;

            return this;
        }

    }
}