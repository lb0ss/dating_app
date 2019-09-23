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
                    opt => opt.MapFrom(src => src.BirthDay.CalculateAge()));
            CreateMap<User, UserForDetailedDto>()
                .ForMember(
                    dest => dest.PhotoUrl, 
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
                .ForMember(
                    dest => dest.Age, 
                    opt => opt.MapFrom(src => src.BirthDay.CalculateAge()));
            CreateMap<Photo, PhotoForDetailedDto>();
        }
    }
}