using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Scrapers
{
    class MemeMakerScraper : ScraperBase
    {
        private int _numberOfPages = 10;
        public MemeMakerScraper()
        {
            Path = "http://alpha-meme-maker.herokuapp.com/";
        }

        public override async Task<List<Meme>> Scrape()
        {
            List<Meme> memes = new();

            try
            {
                for (int i = 0; i < _numberOfPages; i++)
                {
                    HttpResponseMessage response = await client.GetAsync($"{Path}{i}");

                    string jsonString = await response.Content.ReadAsStringAsync();
                    MemeMakerRoot root = JsonSerializer.Deserialize<MemeMakerRoot>(jsonString);

                    if (root.code != 200)
                    {
                        HandleGeneralException(new Exception("MemeMakerScraper Failed to scrape"));
                        return new List<Meme>();
                    }

                    foreach (MemeMakerData data in root.data)
                    {
                        if (string.IsNullOrEmpty(data.tags) ||
                            string.IsNullOrEmpty(data.image) ||
                            string.IsNullOrEmpty(data.topText) ||
                            data.rank == null)
                        {
                            continue;
                        }

                        Meme meme = new Meme(data.topText, data.tags.Split(", ").ToList(), data.image, (int)data.rank, Path, data.ID.ToString());
                        memes.Add(meme);
                    }
                }
                return memes;

            }
            catch (Exception ex)
            {
                HandleGeneralException(ex);
                return memes;
            }
        }
    }

    internal class MemeMakerData
    {
        public int ID { get; set; }
        public string bottomText { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public int? rank { get; set; }
        public string tags { get; set; }
        public string topText { get; set; }
    }

    internal class MemeMakerRoot
    {
        public int code { get; set; }
        public List<MemeMakerData> data { get; set; }
        public string message { get; set; }
    }

}
