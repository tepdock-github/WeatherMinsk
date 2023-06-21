using WeatherMinsk.Domain.DTO;

namespace WeatherMinsk.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<IEnumerable<WeatherDataDTO>> GetPostsAsync();
        Task<WeatherDataDTO> GetPostByIdAsync(long id);
        Task CreatePostAsync(WeatherDataManipulationDTO weatherDataDto);
    }
}
