using System;

namespace ScraperApi.Infrastructure.Utilities
{
    public static class UrlBuilder
    {
        public static List<string> GenerateUrlsForScraping(string baseUrl, string keywords, int numOfResults, int resultsPerPage)
        {
            if (resultsPerPage <= 0)
            {
                throw new Exception("ResultsPerPage must be greater than zero"); // TODO : Add custom exception handling
            }

            if (numOfResults <= 0)
            {
                throw new Exception("NumOfResults must be greater than zero");
            }

            List<string> urls = new List<string>();

            int numOfPages = (int)Math.Ceiling((double)numOfResults / resultsPerPage);

            for (int i = 0; i < numOfPages; i++)
            {
                int startIdx = i * resultsPerPage + 1;
                urls.Add(BuildUrl(baseUrl, keywords, startIdx));
            }

            return urls;
        }

        private static string BuildUrl(string baseUrl, string keywords, int startIdx)
        {
            string uri = Uri.EscapeDataString(keywords);
            return baseUrl.Replace("$$QUERY$$", uri).Replace("$$START$$", startIdx.ToString());
        }

    }
}
