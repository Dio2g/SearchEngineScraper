using ScraperApi.Domain.Models;

namespace ScraperApi.Application.Services
{
    public interface ISearchService
    {
        Task<List<int>> GetSearchPositionsAsync(string keywords, string url, SearchEngineType searchEngine);
    }
}
