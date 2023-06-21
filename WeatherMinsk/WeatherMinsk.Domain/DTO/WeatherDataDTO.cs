namespace WeatherMinsk.Domain.DTO
{
    public class WeatherDataDTO
    {
        public long Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Date { get; set; }
        public string Condition { get; set; }
        public long Humidity { get; set; }
        public long Cloud { get; set; }
        public double TemperatureCelsius { get; set; }
        public double TemperatureFahrenheit { get; set; }
    }
}
