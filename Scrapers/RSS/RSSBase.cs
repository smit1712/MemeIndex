using Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scrapers.RSS
{
    public abstract class RSSBase : IScraper
    {

        public string url;

        abstract public Task<List<Meme>> Scrape();

        public void HandleGeneralException(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
