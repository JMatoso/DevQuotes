using DevQuotes.Communication.Responses;
using DevQuotes.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DevQuotes.Infrastructure.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is Exceptions.ApplicationException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnkownError(context);
        }
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is NotFoundException)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Result = new NotFoundObjectResult(new ResponseErrorJson(context.Exception.Message));
            return;
        }

        if (context.Exception is ErrorOnValidationException)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(context.Exception.Message));
            return;
        }

        if (context.Exception is ConflictException)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            context.Result = new ConflictObjectResult(new ResponseErrorJson(context.Exception.Message));
            return;
        }
    }

    private static void ThrowUnkownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson("Unknown error"));
    }
}
