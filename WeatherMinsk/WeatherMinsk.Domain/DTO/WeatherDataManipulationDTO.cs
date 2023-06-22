using System.ComponentModel.DataAnnotations;

namespace WeatherMinsk.Domain.DTO
{
    public class WeatherDataManipulationDTO
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Condition { get; set; }
        public long Humidity { get; set; }
        public long Cloud { get; set; }
        public double TemperatureCelsius { get; set; }
        public double TemperatureFahrenheit { get; set; }
    }
}
