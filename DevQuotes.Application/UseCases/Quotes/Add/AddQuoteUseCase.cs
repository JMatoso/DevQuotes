﻿using AutoMapper;
using DevQuotes.Application.Extensions;
using DevQuotes.Communication.Requests;
using DevQuotes.Communication.Responses;
using DevQuotes.Domain.Entities;
using DevQuotes.Exceptions;
using DevQuotes.Infrastructure.Repository.Quotes;
using LanguageExt.Common;
using ApplicationException = DevQuotes.Exceptions.ApplicationException;

namespace DevQuotes.Application.UseCases.Quotes.Add;

public sealed class AddQuoteUseCase(IQuotesRepository quotesRepository, IMapper mapper) : IAddQuoteUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly QuoteJsonRequestValidator _validator = new();
    private readonly IQuotesRepository _quotesRepository = quotesRepository;

    public async Task<Result<QuoteResponse>> ExecuteAsync(QuoteRequest newQuote, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(newQuote, cancellationToken);
        if (!validationResult.IsValid)
        {
            var validationErrors = new ApplicationException();
            validationErrors.AddPropertyError("Errors", validationResult.Errors.ToMappedObjects());
            return new Result<QuoteResponse>(validationErrors);
        }

        var quote = _mapper.Map<Quote>(newQuote);
        var result = await _quotesRepository.AddAsync(quote, cancellationToken);

        if (!result.Succeeded)
        {
            var validationErrors = new ApplicationException(result.Message!, ExceptionTypes.Conflict);
            return new Result<QuoteResponse>(validationErrors);
        }

        return _mapper.Map<QuoteResponse>(quote);
    }
}
