using WeatherMinsk.Domain.Entities;

namespace WeatherMinsk.Data.Repositories
{
    public interface IWeatherDataRepository
    {
        Task<IEnumerable<WeatherData>> GetAllPostsAsync();
        Task<WeatherData?> GetPostByIdAsync(long id);
        Task CreatePostAsync(WeatherData entity);
    }
}
