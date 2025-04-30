using ScraperApi.Controllers;
using ScraperApi.Domain.Interfaces;
using ScraperApi.Domain.Models;
using ScraperApi.Infrastructure.Factories;

namespace ScraperApi.Application.Services
{
    public class SearchService : ISearchService
    {
        private readonly IScraperFactory _scraperFactory;
        private readonly ILogger<SearchService> _logger;

        public SearchService(IScraperFactory scraperFactory, ILogger<SearchService> logger)
        {
            _scraperFactory = scraperFactory;
            _logger = logger;
        }

        public async Task<List<int>> GetSearchPositionsAsync(string keywords, string url, SearchEngineType searchEngine)
        {
            if (string.IsNullOrEmpty(keywords))
            {
                throw new ArgumentException("Keywords must not be null or empty");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL must not be null or empty");
            }

            ISearchEngineScraper searchEngineScraper = _scraperFactory.CreateSearchEngineScraper(searchEngine);
            _logger.LogInformation("{engineName} created", searchEngineScraper.GetType().Name);

            return await searchEngineScraper.GetPositionsAsync(keywords, url);
        }
    }
}
