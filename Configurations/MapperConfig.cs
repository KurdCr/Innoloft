using AutoMapper;
using Innoloft.DTOs.Requests;
using Innoloft.DTOs.Responses;
using Innoloft.Models;

namespace Innoloft.Configurations
{
    public class MapperConfig : Profile
    {

        public MapperConfig()
        {
            CreateMap<Event,
                EventDtoRequest>().ReverseMap();

            CreateMap<Event,
               EventDtoResponse>().ReverseMap();

            CreateMap<Event,
           EventParticipantRequest>().ReverseMap();

            CreateMap<Event,
            EventParticipantResponse>().ReverseMap();

            CreateMap<Invitation,
               InvitationDtoRequest>().ReverseMap();

            CreateMap<Invitation,
               InvitationDtoResponse>().ReverseMap();

            CreateMap<User,
               UserDtoRequest>().ReverseMap();

            CreateMap<User,
               UserDtoResponse>().ReverseMap();


        }
    }
    }
