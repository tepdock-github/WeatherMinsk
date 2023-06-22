using Microsoft.Data.Sqlite;
using WeatherMinsk.Domain.Entities;

namespace WeatherMinsk.Data.Repositories
{
    public class WeatherDataRepository : IWeatherDataRepository
    {
        private readonly string _connectionString;

        public WeatherDataRepository(string connectionString) => _connectionString = connectionString;

        public async Task CreatePostAsync(WeatherData entity)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                var query = "INSERT INTO WeatherData " +
                    "(City, Country, Date, Condition, Cloud, Humidity," +
                    " TemperatureCelsius, TemperatureFahrenheit) VALUES" +
                    "(@City, @Country, @Date, @Condition, @Cloud, @Humidity," +
                    "@TemperatureCelsius, @TemperatureFahrenheit)";
                var command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@City", entity.City);
                command.Parameters.AddWithValue("@Country", entity.Country);
                command.Parameters.AddWithValue("@Date", entity.Date);
                command.Parameters.AddWithValue("@Condition", entity.Condition);
                command.Parameters.AddWithValue("@Cloud", entity.Cloud);
                command.Parameters.AddWithValue("@Humidity", entity.Humidity);
                command.Parameters.AddWithValue("@TemperatureCelsius", entity.TemperatureCelsius);
                command.Parameters.AddWithValue("@TemperatureFahrenheit", entity.TemperatureCelsius);

                await connection.OpenAsync();
                await CheckAndCreateTableIfNeeded(connection);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<WeatherData?> GetPostByIdAsync(long id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                var query = "SELECT * FROM WeatherData WHERE Id = @Id";
                var command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();
                await CheckAndCreateTableIfNeeded(connection);
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var weatherData = new WeatherData
                    {
                        Id = (long)reader["Id"],
                        City = (string)reader["City"],
                        Country = (string)reader["Country"],
                        Date = (string)reader["Date"],
                        Condition = (string)reader["Condition"],
                        Cloud = (long)reader["Cloud"],
                        Humidity = (long)reader["Humidity"],
                        TemperatureCelsius = (double)reader["TemperatureCelsius"],
                        TemperatureFahrenheit = (double)reader["TemperatureFahrenheit"]
                    };

                    return weatherData;
                }
            }

            return null;
        }

        public async Task<IEnumerable<WeatherData>> GetAllPostsAsync()
        {
            var weatherDataList = new List<WeatherData>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                var query = "SELECT * FROM WeatherData";
                var command = new SqliteCommand(query, connection);

                await connection.OpenAsync();
                await CheckAndCreateTableIfNeeded(connection);
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var weatherData = new WeatherData
                    {
                        Id = (long)reader["Id"],
                        City = (string)reader["City"],
                        Country = (string)reader["Country"],
                        Date = (string)reader["Date"],
                        Condition = (string)reader["Condition"],
                        Cloud = (long)reader["Cloud"],
                        Humidity = (long)reader["Humidity"],
                        TemperatureCelsius = (double)reader["TemperatureCelsius"],
                        TemperatureFahrenheit = (double)reader["TemperatureFahrenheit"]
                    };

                    weatherDataList.Add(weatherData);
                }
            }

            return weatherDataList;

        }

        private async Task CheckAndCreateTableIfNeeded(SqliteConnection connection)
        {
            var checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='WeatherData'";
            var checkTableCommand = new SqliteCommand(checkTableQuery, connection);
            var tableName = await checkTableCommand.ExecuteScalarAsync() as string;

            if (tableName == null)
            {
                var createTableQuery = "CREATE TABLE WeatherData (" +
                    "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "City TEXT NOT NULL," +
                    "Country TEXT NOT NULL," +
                    "Date TEXT NOT NULL," +
                    "Condition TEXT NOT NULL," +
                    "Cloud INTEGER NOT NULL," +
                    "Humidity INTEGER NOT NULL," +
                    "TemperatureCelsius REAL NOT NULL," +
                    "TemperatureFahrenheit REAL NOT NULL)";
                var createTableCommand = new SqliteCommand(createTableQuery, connection);
                await createTableCommand.ExecuteNonQueryAsync();
            }
        }

    }
}