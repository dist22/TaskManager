using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TaskManager.Domain.Exceptions;

namespace TaskManager.WebAPI.Middleware;

public class ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex switch
            {
                AppException appException => appException.StatusCode,
                _ => StatusCodes.Status500InternalServerError
            };
            
            var response = new
            {
                error = ex.Message,
                status = context.Response.StatusCode,
            };
            
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
            
        }
    }

}