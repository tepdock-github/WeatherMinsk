using Microsoft.AspNetCore.Mvc;
using WeatherMinsk.Services.Interfaces;

namespace WeatherMinsk.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IWeatherPublicService _weatherPublicService;

        public PostsController(IWeatherPublicService weatherPublicService)
        {
            _weatherPublicService = weatherPublicService;
        }

        /// <summary>
        ///     Returns posts about weather either from databse or publicAPI
        /// </summary>
        /// <returns>A list of weather-posts</returns>
        /// <response code="200">Returns list of weather-posts or single post 
        /// about current weather in Minsk</response>
        /// <response code="400">If request to WeatherAPI failed</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet]
        [ResponseCache(Duration = 360, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetAllPostsAsync() =>
            Ok(await _weatherPublicService.GetWeatherDataAsync());
    }
}
