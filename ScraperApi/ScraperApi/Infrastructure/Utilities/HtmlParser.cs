using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Text.RegularExpressions;

namespace ScraperApi.Infrastructure.Utilities
{
    public static class HtmlParser
    {
        public static List<string> ExtractAllResultUrls(string respContent, string regexString)
        {
            List<string> urls = new List<string>();

            // For quick testing
            //string reg = "<cite>(.*?)</cite>";

            Regex regex = new Regex(regexString);
            MatchCollection matches = regex.Matches(respContent);
            //Console.WriteLine("Matches: " + matches.Count);

            foreach (Match match in matches)
            {
                if (match.Success && match.Groups.Count > 1)
                {
                    urls.Add(CleanMatchUrl(match.Groups[1].Value));
                }
            }

            return urls;
        }

        public static List<int> FindPositions(List<string> urls, string desiredUrl)
        {
            List<int> positions = new List<int>();
            for (int i = 0; i < urls.Count; i++)
            {
                if (!string.IsNullOrEmpty(urls[i]) && urls[i].Contains(desiredUrl, StringComparison.OrdinalIgnoreCase))
                {
                    positions.Add(i + 1);
                }
            }

            return positions; 
        }

        private static string CleanMatchUrl(string matchUrl) // TODO : Improve URL cleaning, there is still other bits of junk besides HTML tags
        {
            string cleanUrl = Regex.Replace(matchUrl, "<[^>]+>", ""); // Remove HTML tags

            cleanUrl = cleanUrl.Trim();

            return cleanUrl;
        }
    }
}
