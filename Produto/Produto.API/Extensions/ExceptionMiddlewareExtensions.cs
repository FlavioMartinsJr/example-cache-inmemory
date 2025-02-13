using System.Net;
using System.Text.Json;
using Produto.Application.Exceptions;
using Produto.Domain.Constants;
using Produto.Domain.Enums;
using Microsoft.AspNetCore.Diagnostics;

namespace Produto.API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public record Error(string? StatusCode, string Message, Guid ErrorId)
        {
            public static implicit operator string(Error error) => JsonSerializer.Serialize(error);
        };

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                AllowStatusCode404Response = true,
                ExceptionHandler = async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var errorId = Guid.NewGuid();

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        string errorMessage = string.Empty;
                        string errorCode = string.Empty;

                        if (contextFeature.Error is UserMessageException userMessageException)
                        {
                            switch (userMessageException.ErrorCode)
                            {
                                case ErrorCode.NotFound:
                                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                                    errorMessage = userMessageException.UserMessage;
                                    errorCode = $"{ApplicationConstants.Name}.{ErrorRespondeCodeConstants.NOT_FOUND}";
                                    break;
                                case ErrorCode.VersionConflict:
                                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                                    errorMessage = userMessageException.UserMessage;
                                    errorCode = $"{ApplicationConstants.Name}.{ErrorRespondeCodeConstants.VERSION_CONFLICT}";
                                    break;
                                case ErrorCode.ItemAlreadyExists:
                                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                                    errorMessage = userMessageException.UserMessage;
                                    errorCode = $"{ApplicationConstants.Name}.{ErrorRespondeCodeConstants.ITEM_ALREADY_EXISTS}";
                                    break;
                                case ErrorCode.Conflict:
                                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                                    errorMessage = userMessageException.UserMessage;
                                    errorCode = $"{ApplicationConstants.Name}.{ErrorRespondeCodeConstants.CONFLICT}";
                                    break;
                                case ErrorCode.BadRequest:
                                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                    errorMessage = userMessageException.UserMessage;
                                    errorCode = $"{ApplicationConstants.Name}.{ErrorRespondeCodeConstants.BAD_REQUEST}";
                                    break;
                                case ErrorCode.Unauthorized:
                                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                    errorMessage = userMessageException.UserMessage;
                                    errorCode = $"{ApplicationConstants.Name}.{ErrorRespondeCodeConstants.UNAUTHORIZED}";
                                    break;
                                case ErrorCode.Internal:
                                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                    errorMessage = userMessageException.UserMessage;
                                    errorCode = $"{ApplicationConstants.Name}.{ErrorRespondeCodeConstants.INTERNAL_ERROR}";
                                    break;
                                case ErrorCode.UnprocessableEntity:
                                    context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                                    errorMessage = userMessageException.UserMessage;
                                    errorCode = $"{ApplicationConstants.Name}.{ErrorRespondeCodeConstants.UNPROCESSABLE_ENTITY}";
                                    break;
                                default:
                                    context.Response.StatusCode = 500;
                                    errorMessage = userMessageException.UserMessage;
                                    errorCode = $"{ApplicationConstants.Name}.{ErrorRespondeCodeConstants.GENERAL_ERROR}";
                                    break;
                            }
                        }
                        else
                        {
                            context.Response.StatusCode = 500;
                            errorCode = $"{ApplicationConstants.Name}.{ErrorRespondeCodeConstants.GENERAL_ERROR}";
                            errorMessage = "Erro inesperado, contate o Administrador";
                        }
                        await context.Response.WriteAsync(new Error(errorCode, errorMessage, errorId));
                        logger.LogError("ErrorId:{errorId} Exception:{contextFeature.Error}", errorId, contextFeature.Error);
                    }
                }
            });
        }
    }
}