using System.Net;
using TN.HealthPortal.Logic.DTOs;
using static System.Net.Mime.MediaTypeNames;

namespace TN.HealthPortal.API.Middleware
{
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (ArgumentException exception)
            {
                await HandleExceptionAsync(httpContext, exception, HttpStatusCode.BadRequest);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception, HttpStatusCode.InternalServerError);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode)
        {
            context.Response.ContentType = Application.Json;
            context.Response.StatusCode = (int)httpStatusCode;
            var response = new ErrorDto
            {
                Message = exception.Message,
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
