namespace WeatherMinsk.Domain.DTO
{
    public class WeatherDataManipulationDTO
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Condition { get; set; }
        public int Humidity { get; set; }
        public int Cloud { get; set; }
        public double TemperatureCelsius { get; set; }
        public double TemperatureFahrenheit { get; set; }
    }
}
