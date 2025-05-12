using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestClean.Api.Filters;

public class ErrorHandlingFilterAttributes: ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        // context.Result = new ObjectResult(new { error = "An unexpected error occurred. Please try again later." })
        // {
        //     StatusCode = 500
        // };
        
        var problemDetail = new ProblemDetails 
        {
            Title = "An unexpected error occurred.",
            Detail = exception.Message,
            Status = (int)HttpStatusCode.InternalServerError,
            Type = "https://httpstatuses.com/500",
        };

        context.Result = new ObjectResult(problemDetail);

        context.ExceptionHandled = true;
    }
}