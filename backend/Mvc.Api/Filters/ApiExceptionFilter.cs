using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using DbModel.demoDb;
using System.Security.Claims;

namespace Mvc.Api.Filters
{
    public class ApiExceptionFilter : IActionFilter, IResultFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            // Solo procesar si es un ObjectResult (respuesta con código)
            if (context.Result is ObjectResult objectResult)
            {
                var statusCode = objectResult.StatusCode ?? 200;

                // Solo guardar errores controlados (400, 401, 403, 404)
                // NO guardar 500 porque ya lo maneja el middleware
                if (statusCode >= 400 && statusCode < 500)
                {
                    SaveError(context, statusCode, objectResult.Value?.ToString());
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context) { }

        private void SaveError(FilterContext context, int statusCode, string? message)
        {
            try
            {
                var dbContext = context.HttpContext.RequestServices.GetService<_demoContext>();
                if (dbContext != null)
                {
                    string errorMessage = message ?? "Sin mensaje";

                    // Intentar extraer el mensaje si está en formato JSON
                    if (message?.Contains("message") == true)
                    {
                        try
                        {
                            var json = System.Text.Json.JsonDocument.Parse(message);
                            if (json.RootElement.TryGetProperty("message", out var msgElement))
                            {
                                errorMessage = msgElement.GetString() ?? errorMessage;
                            }
                        }
                        catch { }
                    }

                    var errorLog = new ErrorLog
                    {
                        Message = errorMessage.Length > 2000 ? errorMessage.Substring(0, 2000) : errorMessage,
                        StackTrace = null, // No hay stack trace para errores controlados
                        Path = context.HttpContext.Request.Path,
                        Method = context.HttpContext.Request.Method,
                        UserId = context.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                                 context.HttpContext.User?.FindFirst("sub")?.Value ??
                                 context.HttpContext.User?.FindFirst("unique_name")?.Value ??
                                 "Anonymous",
                        StatusCode = statusCode.ToString(),
                        Timestamp = DateTime.UtcNow
                    };

                    dbContext.ErrorLog.Add(errorLog);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // No hacer nada para no interrumpir la respuesta
                Console.WriteLine($"Error al guardar log controlado: {ex.Message}");
            }
        }
    }
}