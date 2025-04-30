using Microsoft.Extensions.Logging;
using Moq;
using ScraperApi.Application.Services;
using ScraperApi.Domain.Interfaces;
using ScraperApi.Domain.Models;
using ScraperApi.Infrastructure.Factories;

namespace ScraperApi.UnitTests;

[Trait("Category", "Unit")]
public class SearchServiceTests
{
    private readonly SearchService _searchService;
    private readonly Mock<IScraperFactory> _mockFactory;
    private readonly Mock<ISearchEngineScraper> _mockScraper;
    private readonly Mock<ILogger<SearchService>> _mockLogger;

    public SearchServiceTests()
    {
        _mockFactory = new Mock<IScraperFactory>();
        _mockScraper = new Mock<ISearchEngineScraper>();
        _mockLogger = new Mock<ILogger<SearchService>>();
        _mockFactory.Setup(f => f.CreateSearchEngineScraper(It.IsAny<SearchEngineType>())).Returns(_mockScraper.Object);
        _searchService = new SearchService(_mockFactory.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetSearchPositionsAsync_ValidParams_ExpectedPositions()
    {
        // Arrange
        string keywords = "land registry searches";
        string url = "www.infotrack.co.uk";
        SearchEngineType searchEngine = SearchEngineType.Yahoo;
        List<int> expectedPositions = new List<int> { 4, 2, 0 };
        _mockScraper.Setup(s => s.GetPositionsAsync(keywords, url))
            .ReturnsAsync(expectedPositions);

        // Act
        List<int> positions = await _searchService.GetSearchPositionsAsync(keywords, url, searchEngine);

        //Assert
        Assert.Equal(expectedPositions, positions);
    }

    [Fact]
    public async Task GetSearchPositionsAsync_EmptyKeywordsUrl_ThrowsArgumentException()
    {
        // Arrange
        string keywords = "";
        string url = "";
        SearchEngineType searchEngine = SearchEngineType.Bing;

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _searchService.GetSearchPositionsAsync(keywords, url, searchEngine));
    }

    [Fact]
    public async Task GetSearchPositionsAsync_EmptyKeywords_ThrowsArgumentException()
    {
        // Arrange
        string keywords = "";
        string url = "https://testurl.co.uk";
        SearchEngineType searchEngine = SearchEngineType.Bing;

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _searchService.GetSearchPositionsAsync(keywords, url, searchEngine));
    }

    [Fact]
    public async Task GetSearchPositionsAsync_EmptyUrl_ThrowsArgumentException()
    {
        // Arrange
        string keywords = "here are some keywords";
        string url = "";
        SearchEngineType searchEngine = SearchEngineType.Bing;

        // Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _searchService.GetSearchPositionsAsync(keywords, url, searchEngine));
    }

    
}