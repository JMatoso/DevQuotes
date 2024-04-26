using FluentValidation.Results;

namespace DevQuotes.Application.Extensions;

public static class ResultValidatorExtensions
{
    public static IEnumerable<object> ToMappedObjects(this IEnumerable<ValidationFailure> errors)
    {
        foreach (var error in errors)
        {
            yield return new
            {
                Property = error.PropertyName,
                Message = error.ErrorMessage
            };
        }
    }
}
