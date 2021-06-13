using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OrderImport.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderImport.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            object messages = "InternalServerError";

            ProblemDetails problemDetails = null;

            if (exception is DomainException)
            {
                code = HttpStatusCode.UnprocessableEntity;
                IDictionary<string, string[]> errors = (exception as DomainException).ValidationFailures.ToDictionary(t => t.PropertyName, t => new[] { t.ErrorMessage });

                problemDetails = new ValidationProblemDetails(errors)
                {
                    Instance = context.Request.HttpContext.Request.Path,
                    Status = (int)code,
                    Detail = "Please refer to the errors property for additional details"
                };
            }
            else if (exception is Exception)
            {
                code = HttpStatusCode.InternalServerError;

                problemDetails = new ProblemDetails()
                {
                    Title = exception.Message,
                    Status = (int)code,
                    Detail = "Erro Inesperado",
                };
            }

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var result = JsonConvert.SerializeObject(problemDetails, serializerSettings);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
