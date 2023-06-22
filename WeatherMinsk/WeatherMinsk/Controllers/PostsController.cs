using Microsoft.AspNetCore.Mvc;
using WeatherMinsk.Domain.DTO;
using WeatherMinsk.Extensions.Filters;
using WeatherMinsk.Services.Interfaces;

namespace WeatherMinsk.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public PostsController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
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
        public async Task<IActionResult> GetAllPostsAsync() =>
            Ok(await _weatherService.GetPostsAsync());

        /// <summary>
        ///     Returns one post by id, if not found throws exception
        /// </summary>
        /// <param name="id">Id of the post</param>
        /// <returns>Single post</returns>
        /// <response code="200">Returns post by id</response>
        /// <response code="404">If post wasn't found</response>
        /// <response code="500">Internal server Error</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostByIdAsync(long id) =>
            Ok(await _weatherService.GetPostByIdAsync(id));

        /// <summary>
        ///     Creates post
        /// </summary>
        /// <param name="weatherDataDto">Post</param>
        /// <returns></returns>
        /// <response code="204">Operation successful</response>
        /// <response code="422">Failed to validate model</response>
        /// <response code="500">Internal server Error</response>
        [HttpPost]
        [ServiceFilter(typeof(ValidateModelFilter))]
        public async Task<IActionResult> CreatePostAsync([FromBody] WeatherDataManipulationDTO weatherDataDto)
        {
            await _weatherService.CreatePostAsync(weatherDataDto);
            return NoContent();
        }
    }
}
