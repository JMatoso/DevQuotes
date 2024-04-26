using Microsoft.AspNetCore.Mvc;
using LanguageExt.Common;
using DevQuotes.Exceptions;
using DevQuotes.Communication.Responses;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections;

namespace DevQuotes.Api.Controllers;

public static class ControllerExtensions
{
    public static IActionResult ToStatusResult<TResult, TContract>(
        this Result<TResult> result, Func<TResult, TContract> contractFactory)
    {
        return result.Match<IActionResult>(obj =>
        {
            var response = contractFactory(obj);
            return new OkObjectResult(response);
        }, exception =>
        {
            _ = Enum.TryParse(exception.Source, out ExceptionTypes exceptionType);

            if (exceptionType == ExceptionTypes.Unknown)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            var errors = new List<object>();

            foreach (DictionaryEntry entry in exception.Data)
            {
                errors.Add(entry.Value ?? string.Empty);
            }

            var responseError = new ResponseErrorJson(exception.Message, errors);

            return exceptionType switch
            {
                ExceptionTypes.NotFound => new NotFoundObjectResult(responseError),
                ExceptionTypes.BadRequest => new BadRequestObjectResult(responseError),
                ExceptionTypes.Conflict => new ConflictObjectResult(responseError),
                _ => new StatusCodeResult(StatusCodes.Status500InternalServerError)
            };
        });
    }
}
