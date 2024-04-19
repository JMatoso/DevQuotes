using AutoMapper;
using DevQuotes.Communication.Requests;
using DevQuotes.Communication.Responses;
using DevQuotes.Domain.Entities;
using DevQuotes.Exceptions;
using DevQuotes.Infrastructure.Repository;

namespace DevQuotes.Application.UseCases.Quotes.Add;

public class AddQuoteUseCase(IQuotesRepository quotesRepository, IMapper mapper) : IAddQuoteUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IQuotesRepository _quotesRepository = quotesRepository;

    public async Task<QuoteJsonResponse> ExecuteAsync(QuoteJsonRequest newQuote)
    {
        var validator = new QuoteJsonRequestValidator();
        var validationResult = await validator.ValidateAsync(newQuote);
        if (!validationResult.IsValid)
        {
            throw new ErrorOnValidationException("Error adding quote.");
        }

        var quote = _mapper.Map<Quote>(newQuote);
        await _quotesRepository.AddAsync(quote);
        return _mapper.Map<QuoteJsonResponse>(newQuote);
    }
}
