using Serilog;
using WeatherMinsk.ExceptionMiddleware;
using WeatherMinsk.Extensions;
using WeatherMinsk.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var logger = LoggerConfig.CreateLogger(configuration);

try
{
    Log.Logger = logger;

    #region Services
    builder.Services.ConfigureCors();

    builder.Services.AddHttpClient();

    builder.Services.AddAutoMapper(typeof(Program));

    builder.Services.AddControllers();
    builder.Services.AddWeatherPublicService();

    builder.Services.ConfigureVersionig();
    builder.Services.ConfigureSwagger();
    builder.Services.AddSwaggerGen(s =>
    {
        s.IncludeXmlComments("swagger.xml");
    });
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