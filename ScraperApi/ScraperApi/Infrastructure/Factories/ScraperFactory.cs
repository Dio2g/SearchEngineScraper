using ScraperApi.Domain.Interfaces;
using ScraperApi.Domain.Models;
using ScraperApi.Infrastructure.Scrapers;

namespace ScraperApi.Infrastructure.Factories
{
    public class ScraperFactory : IScraperFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ScraperFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISearchEngineScraper CreateSearchEngineScraper(SearchEngineType engineType)
        {
            switch (engineType)
            {
                case SearchEngineType.Bing:
                    return _serviceProvider.GetRequiredService<BingSearchScraper>();
                case SearchEngineType.Yahoo:
                    return _serviceProvider.GetRequiredService<YahooSearchScraper>();
                default:
                    throw new NotSupportedException($"Search engine type: {engineType} does not exist");
            }
        }
    }
}
