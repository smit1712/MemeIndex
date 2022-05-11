using Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scrapers
{
    public abstract class ScraperBase : IScraper
    {
        public string Path;

        protected HttpClient client = new HttpClient();

        abstract public Task<List<Meme>> Scrape();

        public void HandleGeneralException(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
