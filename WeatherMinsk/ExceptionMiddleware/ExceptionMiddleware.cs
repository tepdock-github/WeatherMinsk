using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using WeatherMinsk.ExceptionMiddleware.Exceptions;

namespace WeatherMinsk.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        public readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                int statusCode;
                switch (ex)
                {
                    case BadRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                await WriteErrorMessage(ex, context);
            }
        }

        async Task WriteErrorMessage(Exception ex, HttpContext context)
        {
            var message = ex.Message;
            var details = ex.ToString();

            _logger.LogError(ex, $"An error occured: {message}. Details: {details}");

            var problem = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = message,
                Detail = details
            };

            var stream = context.Response.Body;
            await JsonSerializer.SerializeAsync(stream, problem);
        }
    }
}
