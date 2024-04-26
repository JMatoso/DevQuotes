using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace DevQuotes.Communication.Requests;

public class QuoteJsonRequest()
{
    private string language = default!;
    private string content = default!;

    //[Required]
    //[MaxLength(500)]
    public string Content { get => content; set => content = value.Trim(); }

    //[Required]
    //[MaxLength(25)]
    public string Language { get => language; set => language = value.Trim().ToLower(); }
}

public class QuoteJsonRequestValidator : AbstractValidator<QuoteJsonRequest>
{
    public QuoteJsonRequestValidator()
    {
        RuleFor(x => x.Content).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Language).NotEmpty().MaximumLength(25);
    }
}
