using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using DbModel.demoDb;

namespace Mvc.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, _demoContext dbContext)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado");
                await HandleExceptionAsync(context, ex, dbContext);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, _demoContext dbContext)
        {
            var errorLog = new ErrorLog
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace?.Length > 2000 ? exception.StackTrace.Substring(0, 2000) : exception.StackTrace,
                Path = context.Request.Path,
                Method = context.Request.Method,
                UserId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                         context.User?.FindFirst("sub")?.Value ??
                         "Anonymous",
                StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                Timestamp = DateTime.UtcNow
            };

            try
            {
                await dbContext.ErrorLog.AddAsync(errorLog);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception dbEx)
            {
                _logger.LogError(dbEx, "Error al guardar el log de error en BD");
            }

            var response = new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Ocurrió un error en el servidor",
                ErrorId = errorLog.Id
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}