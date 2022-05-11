using Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Scrapers.RSS
{
    public abstract class NGagRSSBase : RSSBase
    {
        public NGagRSSBase(string url)
        {
            this.url = url;
        }

        public override Task<List<Meme>> Scrape()
        {
            List<Meme> memes = new List<Meme>();

            using var reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            foreach (SyndicationItem item in feed.Items)
            {
                try
                {
                    if (string.IsNullOrEmpty(item.Title.Text) ||
                        item.Categories.Any(c => c.Name == "video")) continue;

                    string description = item.Title.Text.Trim();

                    List<string> meta = new List<string>();

                    meta.AddRange(item.Authors.Select(a => a.Name));
                    meta.AddRange(item.Categories.Select(a => a.Name));
                    string resource = "";
                    int rating = 0;

                    if (item.Summary.Text.Contains("video")) continue;

                    string html = $"<parents>{item.Summary.Text}</parents>";

                    var doc = XDocument.Parse(html);

                    var test = doc.Elements("parents");
                    var firstNode = test.FirstOrDefault().Nodes().FirstOrDefault();
                    var lastNode = test.FirstOrDefault().Nodes().LastOrDefault();


                    if (firstNode.ToString().Contains("img src="))//TODO: improve resource parsing
                    {
                        resource = firstNode.ToString().Replace("<img src=\"", "");
                        resource = resource.Replace("\" />", "");
                        resource = resource.Replace(" ", "");
                    }
                    if (lastNode.ToString().Contains("<p>"))
                    {
                        string points = lastNode.ToString().Split(',').FirstOrDefault();
                        points = points.Replace("<p>", "");
                        points = points.Replace(" points", "");
                        int.TryParse(points, out rating);
                    }

                    memes.Add(new Meme(description, meta, resource, rating, url, item.Id));
                }
                catch (System.Exception ex)
                {

                    throw ex;
                }
            }

            return Task.FromResult(memes);
        }
    }
}
