using Microsoft.AspNetCore.Http;
using MyStore.Core.Mvc.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace MyStore.Core.Mvc.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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
            HttpStatusCode code;
            ErrorResponse error = new ErrorResponse();

           
            if (exception is ValidationException)
            {
                error.Message = "The request model is invalid.";
                error.Content = (exception as ValidationException)?.Errors;
                code = HttpStatusCode.BadRequest;
            }
            else
            {
                code = HttpStatusCode.InternalServerError;
                error.Message = "Internal server error.";
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("development", StringComparison.OrdinalIgnoreCase))
                error.StackTrace = exception.StackTrace;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            var result = JsonConvert.SerializeObject(error, _serializerSettings);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private JsonSerializerSettings _serializerSettings => new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

        public class ErrorResponse
        {
            public string? Message { get; set; }

            public object? Content { get; set; }

            public string? StackTrace { get; set; }

        }

    }
}
