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
        /// Get all quotes.
        /// </summary>
        [HttpGet("all")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<QuoteVM>>> GetAllData()
        {
            return Ok(_mapper.Map<List<QuoteVM>>(await _quoteService.GetAllAsync()));
        }

        /// <summary>
        /// Add quote.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(QuoteVM), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuoteVM>> AddQuote([FromBody] QuoteRequest newQuote)
        {
            if (!ModelState.IsValid) return BadRequest();

            var quote = _mapper.Map<Quote>(newQuote);

            var result = await _quoteService.AddAsync(u => u.Content.Equals(newQuote.Content), quote);

            return result.Successful ?
                CreatedAtAction(nameof(GetQuote), new { qid = quote.Id }, _mapper.Map<QuoteVM>(quote)) :
                StatusCode(result.Code, ActionReporterProvider.Set(result.Message, result.Code));
        }

        /// <summary>
        /// Get quote.
        /// </summary>
        /// <param name="qid">Quote id.</param>
        [HttpGet("{qid}")]
        [ProducesResponseType(typeof(QuoteVM), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuoteVM>> GetQuote(string qid)
        {
            if (string.IsNullOrEmpty(qid))
            {
                return BadRequest(ActionReporterProvider.Set(
                    message: "Insert a valid quote ID.",
                    statusCode: StatusCodes.Status400BadRequest));
            }

            var quote = await _quoteService.GetAsync(u => u.Id == qid.ToString());

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
            var quotes = (await _quoteService.GetAllAsync()).ToList();

            var randomQuote = quotes[new Random().Next(0, await _quoteService.CountAsync())];

            return Ok(new
            {
                Quote = randomQuote.Content
            });
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
        [HttpDelete("{qid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveQuote(string qid)
        {
            if (string.IsNullOrEmpty(qid))
            {
                return BadRequest(ActionReporterProvider.Set(
                    message: "Insert a valid quote ID.",
                    statusCode: StatusCodes.Status400BadRequest));
            }

            var quote = await _quoteService.GetAsync(x => x.Id == qid.ToString());

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
        [HttpPut("{qid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionReporter), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateQuote(string qid, [FromBody]QuoteRequest updateQuote)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (string.IsNullOrEmpty(qid))
            {
                return BadRequest(ActionReporterProvider.Set(
                    message: "Insert a valid quote ID.",
                    statusCode: StatusCodes.Status400BadRequest));
            }

            var quote = await _quoteService.GetAsync(x => x.Id == qid.ToString());

            if (quote is null)
            {
                return NotFound(ActionReporterProvider.Set(
                    message: "Quote not found.",
                    statusCode: StatusCodes.Status404NotFound));
            }

            quote.Content = updateQuote.Content;

            await _quoteService.UpdateAsync(quote);
            return Ok();
        }
    }
}
