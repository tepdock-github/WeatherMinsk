using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using WeatherMinsk.Services.Interfaces;
using WeatherMinsk.Services.Implementation;
using WeatherMinsk.Data;

namespace WeatherMinsk.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });

        public static void ConfigureSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Weather API",
                    Description = "Application for getting weather in Minsk",
                    Version = "v1"
                });
            });

        public static void ConfigureVersionig(this IServiceCollection services) =>
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

        public static void AddWeatherPublicService(this IServiceCollection services) =>
            services.AddScoped<IWeatherPublicService, WeatherPublicService>();

        public static void AddWeatherService(this IServiceCollection services) =>
            services.AddScoped<IWeatherService, WeatherService>();

        public static void ConfigureRepositoryManager(this IServiceCollection services, string connectionString) =>
            services.AddScoped<IRepositoryManager>(provider =>
            new RepositoryManager(connectionString));

    }
}
