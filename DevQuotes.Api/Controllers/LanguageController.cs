using Asp.Versioning;
using DevQuotes.Application.UseCases.Languages.Add;
using DevQuotes.Application.UseCases.Languages.Delete;
using DevQuotes.Application.UseCases.Languages.Get;
using DevQuotes.Application.UseCases.Languages.Update;
using DevQuotes.Communication.Requests;
using DevQuotes.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DevQuotes.Api.Controllers;

/// <summary>
/// Programming Languages Controller.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class LanguageController(IAddLanguageUseCase addLanguageUseCase, IGetLanguagesUseCase getLanguagesUseCase, IDeleteLanguageUseCase deleteLanguageUseCase, IUpdateLanguageUseCase updateLanguageUseCase) : ControllerBase
{
    private readonly IAddLanguageUseCase _addLanguageUseCase = addLanguageUseCase;
    private readonly IGetLanguagesUseCase _getLanguagesUseCase = getLanguagesUseCase;
    private readonly IDeleteLanguageUseCase _deleteLanguageUseCase = deleteLanguageUseCase;
    private readonly IUpdateLanguageUseCase _updateLanguageUseCase = updateLanguageUseCase;

    /// <summary>
    /// Get a list of programming languages.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<LanguageResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<QuoteResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        return Ok(await _getLanguagesUseCase.ExecuteAsync(cancellationToken));
    }

    /// <summary>
    /// Get a programming language using the name and code.
    /// </summary>
    /// <param name="code">Programming Language Code (file extension eg. (cs/py/js)).</param>
    [HttpGet("{code}")]
    [ProducesResponseType(typeof(List<LanguageResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByCodeAsync([FromRoute] string code, CancellationToken cancellationToken)
    {
        var result = await _getLanguagesUseCase.ExecuteAsync(code, cancellationToken);
        return result.ToStatusResult(x => x);
    }

    /// <summary>
    /// Get a programming language (with quotes).
    /// </summary>
    /// <param name="lid">Language id.</param>
    [HttpGet("by/{lid}")]
    [Authorize(Policy = "Not Implemented")]
    [ProducesResponseType(typeof(List<LanguageResponse>), StatusCodes.Status200OK)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid lid, CancellationToken cancellationToken)
    {
        var result = await _getLanguagesUseCase.ExecuteAsync(lid, cancellationToken);
        return result is null ? NotFound(new ResponseErrorJson("Language not found.", [])) : Ok(result);
    }

    /// <summary>
    /// Add new programming language.
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "Not Implemented")]
    [ProducesResponseType(typeof(LanguageResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> AddAsync([FromBody] LanguageRequest newLanguage, CancellationToken cancellationToken)
    {
        var result = await _addLanguageUseCase.ExecuteAsync(newLanguage, cancellationToken);
        return result.ToStatusResult(x => x);
    }

    /// <summary>
    /// Update a programming language.
    /// </summary>
    /// <param name="lid">Language id.</param>
    [HttpPut("{lid}")]
    [Authorize(Policy = "Not Implemented")] 
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid lid, [FromBody] LanguageRequest updateLanguage, CancellationToken cancellationToken)
    {
        var result = await _updateLanguageUseCase.ExecuteAsync(lid, updateLanguage, cancellationToken);
        return result.ToStatusResult(x => x);
    }

    /// <summary>
    /// Delete a programming language.
    /// </summary>
    /// <param name="lid">Language id.</param>
    [HttpDelete("{lid}")]
    [Authorize(Policy = "Not Implemented")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> RemoveAsync([FromRoute] Guid lid, CancellationToken cancellationToken)
    {
        var result = await _deleteLanguageUseCase.ExecuteAsync(lid, cancellationToken);
        return result.ToStatusResult(x => x);
    }
}
