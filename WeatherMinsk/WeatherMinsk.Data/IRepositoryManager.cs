using WeatherMinsk.Data.Repositories;

namespace WeatherMinsk.Data
{
    public interface IRepositoryManager
    {
        IWeatherDataRepository WeatherData { get; }
    }
}
