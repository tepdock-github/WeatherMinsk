using Microsoft.AspNetCore.Mvc;
using Serilog;
using WeatherMinsk.ExceptionMiddleware;
using WeatherMinsk.Extensions;
using WeatherMinsk.Extensions.Configuration;
using WeatherMinsk.Extensions.Filters;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var logger = LoggerConfig.CreateLogger(configuration);

var connectionString = configuration.GetConnectionString("SQLiteConnection");

try
{
    Log.Logger = logger;

    #region Services
    builder.Services.ConfigureCors();
    builder.Services.AddResponseCaching();

    builder.Services.AddHttpClient();

    builder.Services.AddAutoMapper(typeof(Program));

    builder.Services.AddControllers();
    builder.Services.AddWeatherPublicService();
    builder.Services.AddWeatherService();
    builder.Services.ConfigureRepositoryManager(connectionString);

    builder.Services.AddScoped<ValidateModelFilter>();
    builder.Services.Configure<ApiBehaviorOptions>(opt =>
    {
        opt.SuppressModelStateInvalidFilter = true;
    });

    builder.Services.ConfigureVersionig();
    builder.Services.ConfigureSwagger();
    builder.Services.AddSwaggerGen(s =>
    {
        s.IncludeXmlComments("swagger.xml");
    });

    builder.Logging.AddSerilog();
    #endregion

    var app = builder.Build();

    #region Pipeline

    app.UseHsts();
    app.UseHttpsRedirection();

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseStaticFiles();

    app.UseCors("CorsPolicy");

    app.UseRouting();
    app.MapControllers();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.Run();
    #endregion
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}