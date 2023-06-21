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
        [Range(typeof(long), "0", "100")]
        public long Humidity { get; set; }
        [Range(typeof(long), "0", "100")]
        public long Cloud { get; set; }
        [Range(typeof(double), "-273.15", "1000")]
        public double TemperatureCelsius { get; set; }
        [Range(typeof(double), "-459.67", "1832")]
        public double TemperatureFahrenheit { get; set; }
    }
}
