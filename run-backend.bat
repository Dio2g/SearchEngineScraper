@echo off

echo Starting Scraper Backend...
cd ScraperApi\ScraperApi
call dotnet restore
call dotnet build
call dotnet run

pause