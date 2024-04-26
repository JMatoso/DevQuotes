using AutoMapper;
using DevQuotes.Application.Extensions;
using DevQuotes.Communication.Requests;
using DevQuotes.Communication.Responses;
using DevQuotes.Exceptions;
using DevQuotes.Infrastructure.Repository;
using LanguageExt.Common;
using ApplicationException = DevQuotes.Exceptions.ApplicationException;

namespace DevQuotes.Application.UseCases.Quotes.Update;

public class UpdateQuoteUseCase(IQuotesRepository quotesRepository, IMapper mapper) : IUpdateQuoteUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IQuotesRepository _quotesRepository = quotesRepository;

    public async Task<Result<bool>> ExecuteAsync(Guid id, QuoteJsonRequest quote, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            var validationError = new ApplicationException();
            validationError.AddPropertyError(nameof(id), "Id cannot be empty.");
            return new Result<bool>(validationError);
        }

        var quoteToUpdate = await _quotesRepository.GetAsync(id, cancellationToken);
        if (quoteToUpdate is null)
        {
            return new Result<bool>(new ApplicationException("Quote not found", ExceptionTypes.NotFound));
        }

        var validator = new QuoteJsonRequestValidator();
        var validationResult = await validator.ValidateAsync(quote, cancellationToken);
        if (!validationResult.IsValid)
        {
            var validationErrors = new ApplicationException();
            validationErrors.AddPropertyError("Errors", validationResult.Errors.ToMappedObjects());
            return new Result<bool>(validationErrors);
        }

        _mapper.Map(quote, quoteToUpdate);
        var result = await _quotesRepository.UpdateAsync(quoteToUpdate, cancellationToken);

        if (!result.Succeeded)
        {
            return new Result<bool>(new ApplicationException(result.Message!, ExceptionTypes.Conflict));
        }

        return new Result<bool>(true);
    }
}
