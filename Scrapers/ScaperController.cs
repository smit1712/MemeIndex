using Microsoft.Extensions.Logging;
using Scrapers.Events;
using Scrapers.RSS;
using Scrapers.Scrapers.Reddit;
using Shared.Models;
using System;
using System.Collections.Generic;

namespace Scrapers
{
    public class ScaperController
    {
        private List<IScraper> _scrapers = new List<IScraper>();
        private readonly ILogger<ScaperController> _logger;
        public EventHandler<OnScraperDataReady> DataReady;

        public ScaperController(ILogger<ScaperController> logger)
        {
            _logger = logger;
            AddScrapers();
        }

        private void AddScrapers()
        {
            _scrapers.Add(new RedditAniMemesScraper());
            //_scrapers.Add(new RedditDankMemeScraper());
            //_scrapers.Add(new RedditDndMemesScraper());
            //_scrapers.Add(new RedditHistoryMemesScraper());
            //_scrapers.Add(new RedditLotrMemesScraper());
            //_scrapers.Add(new RedditMemeScraper());
            //_scrapers.Add(new RedditPrequelMemeScraper());

            _scrapers.Add(new MemeMakerScraper());
            _scrapers.Add(new NGagAwesomeHot());
            _scrapers.Add(new NGagDefaultHot());
            _scrapers.Add(new NGagFunnyHot());
            _scrapers.Add(new NGagMemeHot());
            _scrapers.Add(new NGagScienceHot());
            _scrapers.Add(new NGagWTFHot());
            _scrapers.Add(new NGagFeed());
        }

        public async void StarScraping()
        {
            Console.WriteLine($"Started scraping process at: {DateTime.Now}");
            foreach (IScraper scraper in _scrapers)
            {
                Console.WriteLine($"Scraper:{scraper.GetType()} started scraping");//TODO: fix logger here
                try
                {
                    List<Meme> memes = await scraper.Scrape();
                    DataReady.Invoke(this, new OnScraperDataReady() { Memes = memes });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to scrape due to:{ex.Message}");
                }

                Console.WriteLine($"Scraper:{scraper.GetType()} stopped scraping");
            }
            Console.WriteLine($"Stopped scraping process at: {DateTime.Now}");
        }
    }
}