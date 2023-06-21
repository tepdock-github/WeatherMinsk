using AutoMapper;
using WeatherMinsk.Domain.DTO;
using WeatherMinsk.Domain.Entities;

namespace WeatherMinsk.Extensions
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<WeatherData, WeatherDataDTO>();
            CreateMap<WeatherDataManipulationDTO, WeatherData>()
            .ForMember(dest => dest.Date, 
                      opt => opt.MapFrom(src => DateTime.Now.ToString("yyyy-MM-dd HH:mm")));
        }
    }
}
