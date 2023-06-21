using WeatherMinsk.Data.Repositories;

namespace WeatherMinsk.Data
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly string _connectionString;
        private IWeatherDataRepository _weatherData;

        public RepositoryManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IWeatherDataRepository WeatherData
        {
            get
            {
                if (_weatherData == null)
                    _weatherData = new WeatherDataRepository(_connectionString);

                return _weatherData;
            }
        }
    }

}
