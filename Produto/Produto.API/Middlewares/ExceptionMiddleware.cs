
namespace Produto.API.Middlewares
{
    public class ExceptionMiddleware(ILoggerFactory logger) : IMiddleware
    {
        private readonly ILogger _logger = logger.CreateLogger<ExceptionMiddleware>();
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError("GlobalExceptionMiddleware: {exception}", ex);
                await context.Response.WriteAsync(ex.ToString());
            }
        }
    }
}
