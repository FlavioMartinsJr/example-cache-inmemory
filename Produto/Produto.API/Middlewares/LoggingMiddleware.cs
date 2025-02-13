using Produto.API.Models;
using System;
using System.Text.Json;

namespace Produto.API.Middlewares
{
    public class LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger _logger = loggerFactory.CreateLogger<LoggingMiddleware>();

        public async Task InvokeAsync(HttpContext context)
        {
            bool isValidFormatRequest = await LogRequest(context);
            if (!isValidFormatRequest)
                await _next(context);
            else
                await LogResponse(context);
        }

        private async Task<bool> LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            var isMultipart = context.Request.HasFormContentType;

            if (isMultipart)
            {
                var form = context.Request.Form;
                foreach (var file in form.Files)
                {
                    ExecuteLogRequest("Upload", logString: $"File: {file.FileName}, Size: {file.Length} bytes");
                }
                return true;
            }
            else
            {
                using MemoryStream memStream = new MemoryStream();
                context.Request.Body.Position = 0;
                await context.Request.Body.CopyToAsync(memStream);
                memStream.Seek(0, SeekOrigin.Begin);
                using var reader = new StreamReader(memStream);
                var requestAsText = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                try
                {
                    //ExecuteLogRequest("Request", logString: JsonSerializer.Serialize(requestAsText));
                    //_logger.LogError("Exception: {exceptionMessage}",context.);
                }
                catch (Exception exception)
                {
                    //ExecuteLogRequest("Request", logString: requestAsText.Replace(Environment.NewLine, string.Empty));
                    _logger.LogError("Exception: {exceptionMessage}", exception.Message.Trim());
                    return false;
                }
                return true;
            }
        }

        private void ExecuteLogRequest(string path, string logString)
        {
            LogModels.LogRequest(_logger, path, logString);
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseAsText = await new StreamReader(responseBody).ReadToEndAsync();
                if (context.Response.ContentType != null && context.Response.ContentType.Contains("application/json"))
                {
                    try
                    {
                        //var response = JsonSerializer.Deserialize<object>(responseAsText);
                        //LogHelper.LogResponse(
                        //    _logger,
                        //    "Response",
                        //    JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true }),
                        //    context.Response.StatusCode
                        //);
                    }
                    catch (JsonException)
                    {
                        LogModels.LogResponse(
                            _logger,
                            "Response",
                            responseAsText,
                            context.Response.StatusCode
                        );
                    }
                }
                else
                {
                    //LogModels.LogResponse(
                    //    _logger,
                    //    "Response",
                    //    responseAsText,
                    //    context.Response.StatusCode
                    //);
                }

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }
}
