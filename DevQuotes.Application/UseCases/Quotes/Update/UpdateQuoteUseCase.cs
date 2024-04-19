using AutoMapper;
using DevQuotes.Communication.Requests;
using DevQuotes.Exceptions;
using DevQuotes.Infrastructure.Repository;

namespace DevQuotes.Application.UseCases.Quotes.Update;

public class UpdateQuoteUseCase(IQuotesRepository quotesRepository, IMapper mapper) : IUpdateQuoteUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IQuotesRepository _quotesRepository = quotesRepository;

    public async Task ExecuteAsync(Guid id, QuoteJsonRequest quote)
    {
        if (id == Guid.Empty)
        {
            throw new ErrorOnValidationException("Id cannot be empty");
        }

        var quoteToUpdate = await _quotesRepository.GetAsync(id) ?? throw new NotFoundException("Quote not found");
        var validator = new QuoteJsonRequestValidator();
        var validationResult = await validator.ValidateAsync(quote);
        if (!validationResult.IsValid)
        {
            throw new ErrorOnValidationException("Error updating quote.");
        }

        _mapper.Map(quote, quoteToUpdate);
        await _quotesRepository.UpdateAsync(quoteToUpdate);
    }
}
