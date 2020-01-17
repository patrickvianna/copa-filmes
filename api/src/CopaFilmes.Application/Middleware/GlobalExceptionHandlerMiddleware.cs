using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace CopaFilmes.Application.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                this.logger.LogCritical($"Ocorreu erro {ex}");
                await this.HandlerAsync(context, ex);
            }
        }

        private async Task HandlerAsync(HttpContext context, Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.HttpContext.Request.Path
            };

            if (ex is BadHttpRequestException badHttpRequestException)
            {
                problemDetails.Title = "Requisicao invalida";
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Detail = badHttpRequestException.Message;
            }
            else if (ex is InvalidOperationException invalidOperationException)
            {
                problemDetails.Title = "Requisicao invalida";
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Detail = invalidOperationException.Message;
            }
            else
            {
                problemDetails.Title = ex.Message;
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                //problemDetails.Detail = ex.ToString();
            }

            context.Response.StatusCode = problemDetails.Status.Value;
            context.Response.ContentType = "application/problem+json";


            var json = JsonSerializer.Serialize(problemDetails);

            await context.Response.WriteAsync(json);
        }
    }
}
