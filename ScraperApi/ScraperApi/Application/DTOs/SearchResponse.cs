namespace ScraperApi.Application.DTOs
{
    public class SearchResponse
    {
        public required IEnumerable<int> Positions { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
