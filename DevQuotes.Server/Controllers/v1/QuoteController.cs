using AutoMapper;
using DevQuotes.Extensions;
using DevQuotes.Extensions.Filters;
using DevQuotes.Extensions.Pagination;
using DevQuotes.Models;
using DevQuotes.Models.Entities;
using DevQuotes.Models.Models;
using DevQuotes.Models.Requests;
using DevQuotes.Server.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DevQuotes.Server.Controllers.v1
{
    /// <summary>
    /// Quotes Controller.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/quote")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public class QuoteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMockRepository<Quote> _quoteService;

        public QuoteController(IMapper mapper, IMockRepository<Quote> quotes)
        {
            _mapper = mapper;
            _quoteService = quotes;
        }

        /// <summary>
        /// Add a quote.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(QuoteVM), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuoteVM>> AddQuote([FromBody] QuoteRequest newQuote)
        {
            if (!ModelState.IsValid) return BadRequest();

            var quote = _mapper.Map<Quote>(newQuote);

            var result = await _quoteService.AddAsync(u => u.Author.Equals(newQuote.Author) && u.Body.Equals(newQuote.Body), quote);

            if (result.Successful)
            {
                return CreatedAtAction(nameof(GetQuote), new { qid = quote.Id }, _mapper.Map<QuoteVM>(quote));
            }

            return StatusCode(result.Code, ActionReporterProvider.Set(result.Message, result.Code));
        }

        /// <summary>
        /// Get quote.
        /// </summary>
        /// <param name="qid">Quote id.</param>
        [HttpGet("{qid:Guid}")]
        [ProducesResponseType(typeof(QuoteVM), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuoteVM>> GetQuote(Guid qid)
        {
            if (Guid.Empty == qid)
            {
                return BadRequest(ActionReporterProvider.Set(
                    message: "Insert a valid quote ID.",
                    statusCode: StatusCodes.Status400BadRequest));
            }

            var quote = await _quoteService.GetAsync(u => u.Id == qid);

            if (quote is null)
            {
                return NotFound(ActionReporterProvider.Set(
                    message: "Quote not found.",
                    statusCode: StatusCodes.Status404NotFound));
            }

            return Ok(_mapper.Map<QuoteVM>(quote));
        }

        /// <summary>
        /// Get a random quote.
        /// </summary>
        [HttpGet("random")]
        [ProducesResponseType(typeof(QuoteVM), StatusCodes.Status200OK)]
        public async Task<ActionResult<QuoteVM>> GetRandomQuote()
        {
            var randomQuote = (await _quoteService.GetAllAsync())
                .ToList()[new Random().Next(0, await _quoteService.CountAsync())];

            return Ok(_mapper.Map<QuoteVM>(randomQuote));
        }

        /// <summary>
        /// Search quote.
        /// </summary>
        /// <param name="keyword">Search keyword.</param>
        [HttpGet("{keyword}")]
        [ProducesResponseType(typeof(List<QuoteVM>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<QuoteVM>>> GetQuoteBy(string keyword, [FromQuery] Parameters parameters)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return BadRequest(ActionReporterProvider.Set(
                    message: "Empty search not allowed.",
                    statusCode: StatusCodes.Status400BadRequest));
            }

            var quotes = await _quoteService.GetAllAsync(
                parameters: parameters,
                expression: x => x.Author.Contains(keyword) || x.Body.Contains(keyword));

            HttpContext.SetDataToHeader<Metadata>("X-Pagination", quotes.Metadata);

            return Ok(quotes);
        }

        /// <summary>
        /// Return a list of quotes.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<QuoteVM>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<QuoteVM>>> GetQuotes([FromQuery] Parameters parameters)
        {
            var quotes = await _quoteService.GetAllAsync(parameters);

            HttpContext.SetDataToHeader<Metadata>("X-Pagination", quotes.Metadata);

            return Ok(quotes);
        }

        /// <summary>
        /// Delete quote.
        /// </summary>
        /// <param name="qid">Quote id.</param>
        [HttpDelete("{qid:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveQuote(Guid qid)
        {
            if (Guid.Empty == qid)
            {
                return BadRequest(ActionReporterProvider.Set(
                    message: "Insert a valid quote ID.",
                    statusCode: StatusCodes.Status400BadRequest));
            }

            var quote = await _quoteService.GetAsync(x => x.Id == qid);

            if (quote is null)
            {
                return NotFound(ActionReporterProvider.Set(
                    message: "Quote not found.",
                    statusCode: StatusCodes.Status404NotFound));
            }

            await _quoteService.RemoveAsync(quote);

            return NoContent();
        }

        /// <summary>
        /// Update quote.
        /// </summary>
        /// <param name="qid">Quote id.</param>
        [HttpPut("{qid:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateQuote(Guid qid, [FromBody]QuoteRequest updateQuote)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (Guid.Empty == qid)
            {
                return BadRequest(ActionReporterProvider.Set(
                    message: "Insert a valid quote ID.",
                    statusCode: StatusCodes.Status400BadRequest));
            }

            var quote = await _quoteService.GetAsync(x => x.Id == qid);

            if (quote is null)
            {
                return NotFound(ActionReporterProvider.Set(
                    message: "Quote not found.",
                    statusCode: StatusCodes.Status404NotFound));
            }

            quote.Body = updateQuote.Body;
            quote.Author = updateQuote.Author;

            await _quoteService.UpdateAsync(quote);
            return Ok();
        }
    }
}
