using Microsoft.AspNetCore.Mvc;

using api.Services;
using api.Domain;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly ISearchEngine _searchEngine;

    public SearchController(ILogger<SearchController> logger, ISearchEngine searchEngine)
    {
        _logger = logger;
        _searchEngine = searchEngine;
    }

    [HttpGet]
    public async Task<ActionResult<ISearchResult<NewsHeadline>>> Get(string searchTerm)
    {
        return Ok(await _searchEngine.SearchAsync(searchTerm));
    }
}
