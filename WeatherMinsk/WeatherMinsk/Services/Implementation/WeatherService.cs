using AutoMapper;
using WeatherMinsk.Data;
using WeatherMinsk.Domain.DTO;
using WeatherMinsk.Domain.Entities;
using WeatherMinsk.ExceptionMiddleware.Exceptions;
using WeatherMinsk.Services.Interfaces;

namespace WeatherMinsk.Services.Implementation
{
    public class WeatherService : IWeatherService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IWeatherPublicService _weatherPublicService;
        private readonly IMapper _mapper;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(IRepositoryManager repositoryManager, IWeatherPublicService weatherPublicService, 
            IMapper mapper, ILogger<WeatherService> logger)
        {
            _repositoryManager = repositoryManager;
            _weatherPublicService = weatherPublicService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<WeatherDataDTO>> GetPostsAsync()
        {
            var posts = await _repositoryManager.WeatherData.GetAllPostsAsync();

            if (!posts.Any()) 
            {
                _logger.LogWarning("There is nothing in database. Request to publicAPI...");

                var weatherDataDto = await _weatherPublicService.GetWeatherDataAsync();
                await CreatePostAsync(weatherDataDto);

                _logger.LogInformation("Post from publicAPI was saved in database");

                var postsAfterRequest = await _repositoryManager.WeatherData.GetAllPostsAsync();
                return _mapper.Map<IEnumerable<WeatherDataDTO>>(postsAfterRequest);
            }

            return _mapper.Map<IEnumerable<WeatherDataDTO>>(posts);
        }

        public async Task<WeatherDataDTO> GetPostByIdAsync(long id)
        {
            var post = await _repositoryManager.WeatherData.GetPostByIdAsync(id);
            if (post == null)
                throw new NotFoundException($"Post with id: {id} wasn't found");

            return _mapper.Map<WeatherDataDTO>(post);
        }

        public async Task CreatePostAsync(WeatherDataManipulationDTO weatherDataDto)
        {
            var weatherData = _mapper.Map<WeatherData>(weatherDataDto);
            await _repositoryManager.WeatherData.CreatePostAsync(weatherData);
        }
    }
}
