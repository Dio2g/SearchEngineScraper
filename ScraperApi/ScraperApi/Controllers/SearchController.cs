using Microsoft.AspNetCore.Mvc;
using ScraperApi.Application.DTOs;
using ScraperApi.Application.Services;

namespace ScraperApi.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ISearchService searchService, ILogger<SearchController> logger)
        {
            _searchService = searchService;
            _logger = logger;
        }

        [HttpPost("positions")]
        public async Task<IActionResult> ScrapeSearchEngine([FromBody] SearchRequest req)
        {
            try 
            {
                _logger.LogInformation("Request received: {Keywords}, {Url}, {SearchEngine}", req.Keywords, req.Url, req.SearchEngine);

                List<int> positions = await _searchService.GetSearchPositionsAsync(req.Keywords, req.Url, req.SearchEngine);

                return Ok(new SearchResponse { Positions = positions });
            } 
            catch (ArgumentException ex) // TODO : Add handling for other exception types
            { 
                _logger.LogError("Invalid arguments were sent: {ex}", ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to process request: {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
