using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherMinsk.ExceptionMiddleware.Exceptions;
using WeatherMinsk.Services.Implementation;
using Xunit;
using WeatherMinsk.Data;
using WeatherMinsk.Domain.DTO;
using WeatherMinsk.Domain.Entities;
using WeatherMinsk.Services.Interfaces;

namespace WeatherMinsk.Tests
{
    public class WeatherServiceTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IWeatherPublicService> _mockWeatherPublicService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<WeatherService>> _mockLogger;
        private readonly WeatherService _weatherService;

        public WeatherServiceTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();
            _mockWeatherPublicService = new Mock<IWeatherPublicService>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<WeatherService>>();
            _weatherService = new WeatherService(
                _mockRepositoryManager.Object,
                _mockWeatherPublicService.Object,
                _mockMapper.Object,
                _mockLogger.Object
            );
        }

        [Fact]
        public async Task GetPostsAsync_WhenPostsExist_ReturnsPosts()
        {
            // Arrange
            var mockPosts = new List<WeatherData>();
            _mockRepositoryManager.Setup(repo => repo.WeatherData.GetAllPostsAsync())
                .ReturnsAsync(mockPosts);

            var expectedPosts = new List<WeatherDataDTO>();
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<WeatherDataDTO>>(mockPosts))
                .Returns(expectedPosts);

            // Act
            var result = await _weatherService.GetPostsAsync();

            // Assert
            Assert.Equal(expectedPosts, result);
        }

        [Fact]
        public async Task GetPostsAsync_WhenNoPostsExist_CallsWeatherPublicService()
        {
            // Arrange
            _mockRepositoryManager.Setup(repo => repo.WeatherData.GetAllPostsAsync())
                .ReturnsAsync(new List<WeatherData>());

            // Act
            await _weatherService.GetPostsAsync();

            // Assert
            _mockWeatherPublicService.Verify(service => service.GetWeatherDataAsync(), Times.Once);
        }

        [Fact]
        public async Task GetPostsAsync_WhenNoPostsExist_CreatesPost()
        {
            // Arrange
            _mockRepositoryManager.Setup(repo => repo.WeatherData.GetAllPostsAsync())
                .ReturnsAsync(new List<WeatherData>());

            _mockRepositoryManager.Setup(repo => repo.WeatherData.CreatePostAsync(It.IsAny<WeatherData>()))
                .Returns(Task.CompletedTask);

            // Act
            await _weatherService.GetPostsAsync();

            // Assert
            _mockRepositoryManager.Verify(repo => repo.WeatherData.CreatePostAsync(It.IsAny<WeatherData>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetPostByIdAsync_WithExistingPostId_ReturnsExpectedDto(long postId)
        {
            // Arrange
            var mockPost = new WeatherData();
            _mockRepositoryManager.Setup(repo => repo.WeatherData.GetPostByIdAsync(postId))
                .ReturnsAsync(mockPost);

            var expectedPostDto = new WeatherDataDTO();
            _mockMapper.Setup(mapper => mapper.Map<WeatherDataDTO>(mockPost))
                .Returns(expectedPostDto);

            // Act
            var result = await _weatherService.GetPostByIdAsync(postId);

            // Assert
            Assert.Equal(expectedPostDto, result);
            _mockRepositoryManager.Verify(repo => repo.WeatherData.GetPostByIdAsync(postId), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<WeatherDataDTO>(mockPost), Times.Once);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(2.5)]
        public async Task GetPostByIdAsync_WithNonExistingPostId_ThrowsNotFoundException(long postId)
        {
            // Arrange
            _mockRepositoryManager.Setup(repo => repo.WeatherData.GetPostByIdAsync(postId))
                .ReturnsAsync((WeatherData)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _weatherService.GetPostByIdAsync(postId));
            _mockRepositoryManager.Verify(repo => repo.WeatherData.GetPostByIdAsync(postId), Times.Once);
        }

        [Fact]
        public async Task CreatePostAsync_CallsRepositoryManagerToCreatePost()
        {
            // Arrange
            var weatherDataDto = new WeatherDataManipulationDTO();
            var weatherData = new WeatherData();
            _mockMapper.Setup(mapper => mapper.Map<WeatherData>(weatherDataDto))
                .Returns(weatherData);

            _mockRepositoryManager.Setup(repo => repo.WeatherData.CreatePostAsync(weatherData))
                .Returns(Task.CompletedTask);

            // Act
            await _weatherService.CreatePostAsync(weatherDataDto);

            // Assert
            _mockRepositoryManager.Verify(repo => repo.WeatherData.CreatePostAsync(weatherData), Times.Once);
        }
    }

}
