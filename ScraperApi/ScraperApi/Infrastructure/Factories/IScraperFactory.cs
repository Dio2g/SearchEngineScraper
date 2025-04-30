using ScraperApi.Domain.Interfaces;
using ScraperApi.Domain.Models;

namespace ScraperApi.Infrastructure.Factories
{
    public interface IScraperFactory
    {
        ISearchEngineScraper CreateSearchEngineScraper(SearchEngineType engineType);
    }
}
