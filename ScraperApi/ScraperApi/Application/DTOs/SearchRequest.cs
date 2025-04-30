using ScraperApi.Domain.Models;

namespace ScraperApi.Application.DTOs
{
    public class SearchRequest
    {
        public required string Keywords { get; set; }
        public required string Url { get; set; }
        public required SearchEngineType SearchEngine { get; set; }
    }
}
