using FluentValidation;

namespace DevQuotes.Communication.Requests;

public sealed class QuoteRequest()
{
    public string Content { get; set; } = string.Empty;
    public Guid LanguageId { get; set; }
}

public sealed class QuoteJsonRequestValidator : AbstractValidator<QuoteRequest>
{
    public QuoteJsonRequestValidator()
    {
        RuleFor(x => x.Content).NotEmpty().MaximumLength(2500);
        RuleFor(x => x.LanguageId).NotEmpty();
    }
}
