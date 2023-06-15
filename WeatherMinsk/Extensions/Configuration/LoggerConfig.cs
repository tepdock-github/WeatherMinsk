using Serilog;
using ILogger = Serilog.ILogger;

namespace WeatherMinsk.Extensions.Configuration
{
    public static class LoggerConfig
    {
        public static ILogger CreateLogger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return Log.Logger;
        }
    }
}
