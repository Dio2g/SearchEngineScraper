using ScraperApi.Application.Services;
using ScraperApi.Infrastructure.Factories;
using ScraperApi.Infrastructure.Scrapers;
using ScraperApi.Infrastructure.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_react",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IScraperFactory, ScraperFactory>();
builder.Services.AddScoped<BingSearchScraper>();
builder.Services.AddScoped<YahooSearchScraper>();

builder.Services.AddHttpClient("Scraper", httpClient =>
{
    // Headers taken from firefox
    httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
    httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br, zstd");
    httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-GB,en;q=0.5");
    httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
    httpClient.DefaultRequestHeaders.Add("DNT", "1");
    httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
    httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
    httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
    httpClient.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
    httpClient.DefaultRequestHeaders.Add("Sec-GPC", "1");
    httpClient.DefaultRequestHeaders.Add("TE", "Trailers");
    //httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgentProvider.GetRandomUserAgent());
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    AutomaticDecompression = System.Net.DecompressionMethods.All
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("_react");

app.UseAuthorization();

app.MapControllers();

app.Run();