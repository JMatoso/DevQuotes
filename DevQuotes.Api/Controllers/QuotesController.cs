using Asp.Versioning;
using DevQuotes.Application.UseCases.Quotes.Add;
using DevQuotes.Application.UseCases.Quotes.Delete;
using DevQuotes.Application.UseCases.Quotes.GetAll;
using DevQuotes.Application.UseCases.Quotes.GetById;
using DevQuotes.Application.UseCases.Quotes.GetLanguages;
using DevQuotes.Application.UseCases.Quotes.GetRandom;
using DevQuotes.Application.UseCases.Quotes.Update;
using DevQuotes.Communication.Requests;
using DevQuotes.Communication.Responses;
using DevQuotes.Shared.Extensions;
using DevQuotes.Infrastructure.Helpers.Pagination;
using DevQuotes.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DevQuotes.Api.Controllers;

/// <summary>
/// Quotes Controller.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class QuotesController(IAddQuoteUseCase addQuoteUseCase, IGetAllQuotesUseCase getQuotesUseCase, IDeleteQuoteUseCase deleteQuoteUseCase, IUpdateQuoteUseCase updateQuoteUseCase, IGetQuoteByIdUseCase getQuoteByIdUseCase, IGetLanguagesUseCase getLanguagesUseCase, IGetRandomQuoteUseCase getRandomQuoteUseCase) : ControllerBase
{
    private readonly IAddQuoteUseCase _addQuoteUseCase = addQuoteUseCase;
    private readonly IGetAllQuotesUseCase _getQuotesUseCase = getQuotesUseCase;
    private readonly IDeleteQuoteUseCase _deleteQuoteUseCase = deleteQuoteUseCase;
    private readonly IUpdateQuoteUseCase _updateQuoteUseCase = updateQuoteUseCase;
    private readonly IGetQuoteByIdUseCase _getQuoteByIdUseCase = getQuoteByIdUseCase;
    private readonly IGetLanguagesUseCase _getLanguagesUseCase = getLanguagesUseCase;
    private readonly IGetRandomQuoteUseCase _getRandomQuoteUseCase = getRandomQuoteUseCase;

    /// <summary>
    /// Get quote.
    /// </summary>
    /// <param name="qid">Quote id.</param>
    [HttpGet("{qid}")]
    [ProducesResponseType(typeof(QuoteJsonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAsync([FromRoute]Guid qid, CancellationToken cancellationToken)
    {
        var result = await _getQuoteByIdUseCase.ExecuteAsync(qid, cancellationToken);
        return result.ToStatusResult(x => x);
    }

    /// <summary>
    /// Get a random quote.
    /// </summary>
    [HttpGet("random")]
    [ProducesResponseType(typeof(QuoteJsonResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<QuoteJsonResponse>> GetRandomAsync(CancellationToken cancellationToken)
        => Ok(await _getRandomQuoteUseCase.ExecuteAsync(cancellationToken));

    /// <summary>
    /// Get a list of quotes.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<QuoteJsonResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<QuoteJsonResponse>>> GetAllAsync([FromQuery] Parameters parameters, CancellationToken cancellationToken, [FromQuery] string search = "")
    {
        var result = await _getQuotesUseCase.ExecuteAsync(parameters, search, cancellationToken);
        HttpContext.SetDataToHeader<Metadata>("X-Pagination", result.Metadata);
        return Ok(result.Quotes);
    }

    /// <summary>
    /// Get a list of quotes languages.
    /// </summary>
    [HttpGet("languages")]
    [ProducesResponseType(typeof(List<QuoteLanguageJsonResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<QuoteJsonResponse>>> GetQuoteLanguagesAsync(CancellationToken cancellationToken)
        => Ok(await _getLanguagesUseCase.ExecuteAsync(cancellationToken));

    /// <summary>
    /// Add quote.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(QuoteJsonResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync([FromBody] QuoteJsonRequest newQuote, CancellationToken cancellationToken)
    {
        var result = await _addQuoteUseCase.ExecuteAsync(newQuote, cancellationToken);
        return result.ToStatusResult(x => x);
    }

    /// <summary>
    /// Update quote.
    /// </summary>
    /// <param name="qid">Quote id.</param>
    [HttpPut("{qid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateQuote([FromRoute] Guid qid, [FromBody] QuoteJsonRequest updateQuote, CancellationToken cancellationToken)
    {
        var result = await _updateQuoteUseCase.ExecuteAsync(qid, updateQuote, cancellationToken);
        return result.ToStatusResult(x => x);
    }

    /// <summary>
    /// Delete quote.
    /// </summary>
    /// <param name="qid">Quote id.</param>
    [HttpDelete("{qid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveAsync([FromRoute] Guid qid, CancellationToken cancellationToken)
    {
        var result = await _deleteQuoteUseCase.ExecuteAsync(qid, cancellationToken);
        return result.ToStatusResult(x => x);
    }
}
