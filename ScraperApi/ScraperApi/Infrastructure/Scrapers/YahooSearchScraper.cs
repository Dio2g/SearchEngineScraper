using ScraperApi.Domain.Interfaces;

namespace ScraperApi.Infrastructure.Scrapers
{
    public class YahooSearchScraper : ISearchEngineScraper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<YahooSearchScraper> _logger;
        private readonly string _baseUrl;
        private readonly string _regex;
        private readonly int _numOfResults;
        private readonly int _resultsPerPage;

        public YahooSearchScraper(IHttpClientFactory httpClientFactory, IConfiguration config, ILogger<YahooSearchScraper> logger)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

            _baseUrl = config["SearchEngineUrls:Yahoo:BaseUrl"] ?? "";
            _regex = config["SearchEngineUrls:Yahoo:Regex"] ?? "";
            _numOfResults = Convert.ToInt32(config["SearchEngineUrls:Yahoo:NumOfResults"]);
            _resultsPerPage = Convert.ToInt32(config["SearchEngineUrls:Yahoo:ResultsPerPage"]);
        }

        public async Task<List<int>> GetPositionsAsync(string keywords, string url)
        {
            // TODO: Implement
            throw new NotImplementedException("Yahoo scraper is not implemented yet");
        }
    }
}
