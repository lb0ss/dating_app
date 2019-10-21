using System.Linq;
using AutoMapper;
using dating_app.api.Dtos;
using dating_app.api.Models;

namespace dating_app.api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(
                    dest => dest.PhotoUrl, 
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(
                    dest => dest.Age, 
                    opt => opt.MapFrom(src => src.DateofBirth.CalculateAge()));
            CreateMap<User, UserForDetailedDto>()
                .ForMember(
                    dest => dest.PhotoUrl, 
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                 .ForMember(
                    dest => dest.Age, 
                    opt => opt.MapFrom(src => src.DateofBirth.CalculateAge()));
            CreateMap<Photo, PhotoForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<MessageForCreationDto, Message>().ReverseMap();
            CreateMap<Message, MessagesToReturnDto>()       // manually map the SenderPhotoUrl to MessagesToReturnDto
                .ForMember(m => m.SenderPhotoUrl, 
                    opt => opt.MapFrom(u => u.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(m => m.RecipientPhotoUrl, 
                    opt => opt.MapFrom(u => u.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
        }
    }
}