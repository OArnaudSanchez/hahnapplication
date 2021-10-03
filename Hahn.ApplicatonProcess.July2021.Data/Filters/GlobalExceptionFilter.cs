using Hahn.ApplicatonProcess.July2021.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Hahn.ApplicatonProcess.July2021.Data.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(BussinessException))
            {
                var exception = (BussinessException)context.Exception;
                var validation = new
                {
                    Title = "Exception",
                    Message = exception.message,
                    StatusCode = exception.statusCode
                };

                var jsonError = new
                {
                    errors = new[] { validation }
                };

                if (exception.statusCode == 404)
                {
                    context.Result = new NotFoundObjectResult(jsonError);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }
                else
                {
                    context.Result = new BadRequestObjectResult(jsonError);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.ExceptionHandled = true;
                }
            }
        }
    }
}
