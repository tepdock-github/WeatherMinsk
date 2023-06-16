using WeatherMinsk.Domain.DTO;

namespace WeatherMinsk.Services.Interfaces
{
    public interface IWeatherPublicService
    {
        Task<IEnumerable<WeatherDataDTO>> GetWeatherDataAsync();
    }
}
