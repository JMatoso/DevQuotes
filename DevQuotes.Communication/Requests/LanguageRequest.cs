using FluentValidation;

namespace DevQuotes.Communication.Requests;

public sealed class LanguageRequest
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}

public sealed class LanguageRequestValidator : AbstractValidator<LanguageRequest>
{
    public LanguageRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Code).NotEmpty().MaximumLength(5);
    }
}
