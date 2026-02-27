using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace HealthWellness.Services
{
    public class MediumService
    {
        public async Task<List<object>> GetArticles()
        {
            var rssUrl = "https://medium.com/feed/@aniketkachhaway";

            using var reader = XmlReader.Create(rssUrl);
            var feed = SyndicationFeed.Load(reader);

            var articles = feed.Items.OrderByDescending(x => x.PublishDate).Select(item =>
            {
                var content = item.Summary?.Text ?? "";

                // 🔥 Extract first image from HTML
                var imageMatch = Regex.Match(content, "<img.*?src=\"(.*?)\"", RegexOptions.IgnoreCase);
                var imageUrl = imageMatch.Success ? imageMatch.Groups[1].Value : null;

                return new
                {
                    title = item.Title.Text,
                    link = item.Links.FirstOrDefault()?.Uri.ToString(),
                    published = item.PublishDate.DateTime,
                    description = StripHtml(content),
                    image = imageUrl
                };
            }).ToList<object>();

            return articles;
        }

        // 🔥 Remove HTML tags for clean preview text
        private static string StripHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}