using MediatR;
using MilkNest.Application.Common.Exceptions;
using MilkNest.Domain;
using System.Net;
using System.Text.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace MilkNest.Server.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionHandlerMiddleware(RequestDelegate Next) =>
            this.next = Next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { errors = validationException.Value });
                    break;

                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new { error = notFoundException.Message });
                    break;

                case UnauthorizedAccessException unauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized;
                    result = JsonSerializer.Serialize(new { error = unauthorizedAccessException.Message });
                    break;

                case InvalidOperationException invalidOperationException:
                    code = HttpStatusCode.Conflict;
                    result = JsonSerializer.Serialize(new { error = invalidOperationException.Message });
                    break;

                case ArgumentNullException argumentNullException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { error = argumentNullException.Message });
                    break;

                case ArgumentException argumentException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { error = argumentException.Message });
                    break;

                case FormatException formatException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { error = formatException.Message });
                    break;

                case TimeoutException timeoutException:
                    code = HttpStatusCode.GatewayTimeout; 
                    result = JsonSerializer.Serialize(new { error = timeoutException.Message });
                    break;

                case OperationCanceledException operationCanceledException:
                    code = HttpStatusCode.RequestTimeout; 
                    result = JsonSerializer.Serialize(new { error = operationCanceledException.Message });
                    break;

              

                case NotImplementedException notImplementedException:
                    code = HttpStatusCode.NotImplemented; 
                    result = JsonSerializer.Serialize(new { error = notImplementedException.Message });
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }
            return context.Response.WriteAsync(result);
        }
    }
}