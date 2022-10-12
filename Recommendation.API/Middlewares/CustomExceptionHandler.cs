using Newtonsoft.Json;
using Recommendation.API.Interfaces;
using System.Diagnostics;
using System.Net;

namespace Recommendation.API.Middlewares
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ICustomLogger _logger;
        public CustomExceptionHandler(RequestDelegate next, ICustomLogger loggerService)
        {
            _next = next;
            _logger = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();

            try
            {
                string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
                _logger.Write(message);

                await _next.Invoke(context);
                //await _next(context);

                watch.Stop();
                message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded." + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
                _logger.Write(message);

            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[ERROR] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            _logger.Write(message);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

            return context.Response.WriteAsync(result);
        }
    }

    //Customize bir middleware yazdığımızda bu middleware'in kullanılabilmesi için bir extension metot yazılması gerekir.
    //bu metotta builder.UseMiddleware metotu ile hangi classı middleware olarak dikkate alacağımızı söyleriz.
    public static class CustomExceptionHandlerMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandler>();
        }
    }
}
