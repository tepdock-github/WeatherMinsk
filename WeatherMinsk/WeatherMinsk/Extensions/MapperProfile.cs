using AutoMapper;
using WeatherMinsk.Domain.DTO;
using WeatherMinsk.Domain.Entities;

namespace WeatherMinsk.Extensions
{
    public class MapperProfile : Profile
    {
        protected MapperProfile()
        {
            CreateMap<WeatherData, WeatherDataDTO>();
            CreateMap<WeatherDataManipulationDTO, WeatherData>();
        }
    }
}
