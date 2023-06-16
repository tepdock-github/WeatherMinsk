using Newtonsoft.Json.Linq;
using WeatherMinsk.Domain.DTO;
using WeatherMinsk.ExceptionMiddleware.Exceptions;
using WeatherMinsk.Services.Interfaces;

namespace WeatherMinsk.Services.Implementation
{
    public class WeatherPublicService : IWeatherPublicService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeatherPublicService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<WeatherDataDTO> GetWeatherDataAsync()
        {
            var apiSettings = _configuration.GetSection("WeatherAPI");
            var key = apiSettings.GetSection("Key").Value;
            var location = apiSettings.GetSection("Location").Value;
            var baseUrl = apiSettings.GetSection("BaseUrl").Value;

            var url = baseUrl + $"?key={key}&q={location}&aqi=no";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new BadRequestException("Failed to get response from GetWeatherDataAsync method");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var weatherData = await ParseJsonString(jsonString);

            return weatherData;
        }

        private async Task<WeatherDataDTO> ParseJsonString(string json)
        {
            var jsonObject = JObject.Parse(json);

            var name = jsonObject["location"]?["name"]?.ToString();
            var country = jsonObject["location"]?["country"]?.ToString();
            var conditionText = jsonObject["current"]?["condition"]?["text"]?.ToString();
            var humidity = jsonObject["current"]?["humidity"]?.Value<int>() ?? 0;
            var cloud = jsonObject["current"]?["cloud"]?.Value<int>() ?? 0;
            var tempC = jsonObject["current"]?["temp_c"]?.Value<double>() ?? 0.0;
            var tempF = jsonObject["current"]?["temp_f"]?.Value<double>() ?? 0.0;

            var weatherDataDTO = new WeatherDataDTO
            {
                City = name,
                Country = country,
                Condition = conditionText,
                Humidity = humidity,
                Cloud = cloud,
                TemperatureCelsius = tempC,
                TemperatureFahrenheit = tempF
            };

            return await Task.FromResult(weatherDataDTO);
        }
    }
}
