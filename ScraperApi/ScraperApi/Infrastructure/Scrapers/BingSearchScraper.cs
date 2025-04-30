using ScraperApi.Domain.Interfaces;
using ScraperApi.Infrastructure.Utilities;
using System.Collections.Concurrent;
using System.Net.Http;

namespace ScraperApi.Infrastructure.Scrapers
{
    public class BingSearchScraper : ISearchEngineScraper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BingSearchScraper> _logger;
        private readonly string _baseUrl;
        private readonly string _regex;
        private readonly int _numOfResults;
        private readonly int _resultsPerPage;
        private const int maxDegreeOfParallelism = 3;

        public BingSearchScraper(IHttpClientFactory httpClientFactory, IConfiguration config, ILogger<BingSearchScraper> logger)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

            _baseUrl = config["SearchEngineUrls:Bing:BaseUrl"];
            _regex = config["SearchEngineUrls:Bing:Regex"]; 
            _numOfResults = Convert.ToInt32(config["SearchEngineUrls:Bing:NumOfResults"]);
            _resultsPerPage = Convert.ToInt32(config["SearchEngineUrls:Bing:ResultsPerPage"]);
        }

        public async Task<List<int>> GetPositionsAsync(string keywords, string url)
        {

            ConcurrentDictionary<int, List<string>> allResultUrlsDict = new ConcurrentDictionary<int, List<string>>();

            List<string> urlsToScrape = UrlBuilder.GenerateUrlsForScraping(_baseUrl, keywords, _numOfResults, _resultsPerPage);

            //HttpClient _httpClient = _httpClientFactory.CreateClient("Scraper");

            List<Task> tasks = new List<Task>();


            for (int i = 0; i < urlsToScrape.Count; i++)
            {
                int index = i;
                tasks.Add(Task.Run(async () =>
                {
                    HttpClient httpClient = _httpClientFactory.CreateClient("Scraper");

                    string userAgent = UserAgentProvider.GetRandomUserAgent();
                    httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgentProvider.GetRandomUserAgent());

                    _logger.LogInformation("Scraping URL: {url}", urlsToScrape[index]);

                    HttpResponseMessage resp = await httpClient.GetAsync(urlsToScrape[index]);

                    if (!resp.IsSuccessStatusCode)
                    {
                        _logger.LogError("Failed to scrape URL: {url}", urlsToScrape[index]);
                        resp.EnsureSuccessStatusCode();
                    }

                    string respContent = await resp.Content.ReadAsStringAsync();
                    List<string> extractedUrls = HtmlParser.ExtractAllResultUrls(respContent, _regex);

                    allResultUrlsDict.TryAdd(index, extractedUrls);
                }));
            }

            await Task.WhenAll(tasks);

            List<string> allResultUrls = allResultUrlsDict
                .OrderBy(url => url.Key)
                .SelectMany(url => url.Value)
                .ToList();

            if (allResultUrls.Count > 100) // Capped at 100, Bing isnt always accurate with results per page (usually 10) but sometimes it changes dynamically
            {
                allResultUrls = allResultUrls.Take(100).ToList();
            }

            List<int> positions = HtmlParser.FindPositions(allResultUrls, url);

            return positions;
        }
    }
}
