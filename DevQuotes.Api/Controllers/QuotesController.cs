using Asp.Versioning;
using DevQuotes.Application.UseCases.Quotes.Add;
using DevQuotes.Application.UseCases.Quotes.Delete;
using DevQuotes.Application.UseCases.Quotes.Update;
using DevQuotes.Communication.Requests;
using DevQuotes.Communication.Responses;
using DevQuotes.Shared.Extensions;
using DevQuotes.Infrastructure.Helpers.Pagination;
using DevQuotes.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using DevQuotes.Application.UseCases.Quotes.Get;
using Microsoft.AspNetCore.Authorization;

namespace DevQuotes.Api.Controllers;

/// <summary>
/// Quotes Controller.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class QuotesController(IAddQuoteUseCase addQuoteUseCase, IGetQuotesUseCase getQuotesUseCase, IDeleteQuoteUseCase deleteQuoteUseCase, IUpdateQuoteUseCase updateQuoteUseCase) : ControllerBase
{
    private readonly IAddQuoteUseCase _addQuoteUseCase = addQuoteUseCase;
    private readonly IGetQuotesUseCase _getQuotesUseCase = getQuotesUseCase;
    private readonly IDeleteQuoteUseCase _deleteQuoteUseCase = deleteQuoteUseCase;
    private readonly IUpdateQuoteUseCase _updateQuoteUseCase = updateQuoteUseCase;

    /// <summary>
    /// Get quote.
    /// </summary>
    /// <param name="qid">Quote id.</param>
    [HttpGet("{qid}")]
    [ProducesResponseType(typeof(QuoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAsync([FromRoute]Guid qid, CancellationToken cancellationToken)
    {
        var result = await _getQuotesUseCase.ExecuteAsync(qid, cancellationToken);
        return result.ToStatusResult(x => x);
    }

    /// <summary>
    /// Get a random quote.
    /// </summary>
    [HttpGet("random")]
    [ProducesResponseType(typeof(QuoteResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRandomAsync(CancellationToken cancellationToken)
    {
        var result = await _getQuotesUseCase.ExecuteAsync(cancellationToken);
        return result.ToStatusResult(x => x);
    }

    /// <summary>
    /// Get a list of quotes.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<QuoteResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<QuoteResponse>>> GetAllAsync([FromQuery] PaginationParameters parameters, CancellationToken cancellationToken, [FromQuery] string search = "")
    {
        var result = await _getQuotesUseCase.ExecuteAsync(parameters, search, cancellationToken);
        HttpContext.SetDataToHeader<Metadata>("X-Pagination", result.Metadata);
        return Ok(result.Quotes);
    }

    /// <summary>
    /// Add a quote.
    /// </summary>
    [HttpPost]
    [Authorize] // Lol
    [ProducesResponseType(typeof(QuoteResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync([FromBody] QuoteRequest newQuote, CancellationToken cancellationToken)
    {
        var result = await _addQuoteUseCase.ExecuteAsync(newQuote, cancellationToken);
        return result.ToStatusResult(x => x);
    }

    /// <summary>
    /// Update a quote.
    /// </summary>
    /// <param name="qid">Quote id.</param>
    [HttpPut("{qid}")]
    [Authorize] // Lol
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid qid, [FromBody] QuoteRequest updateQuote, CancellationToken cancellationToken)
    {
        var result = await _updateQuoteUseCase.ExecuteAsync(qid, updateQuote, cancellationToken);
        return result.ToStatusResult(x => x);
    }

    /// <summary>
    /// Delete a quote.
    /// </summary>
    /// <param name="qid">Quote id.</param>
    [HttpDelete("{qid}")]
    [Authorize] // Lol
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveAsync([FromRoute] Guid qid, CancellationToken cancellationToken)
    {
        var result = await _deleteQuoteUseCase.ExecuteAsync(qid, cancellationToken);
        return result.ToStatusResult(x => x);
    }
}
