namespace ScraperApi.Domain.Interfaces
{
    public interface ISearchEngineScraper
    {
        Task<List<int>> GetPositionsAsync(string keywords, string url);
    }
}
