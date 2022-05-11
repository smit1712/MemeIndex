using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scrapers
{
    public interface IScraper
    {
        Task<List<Meme>> Scrape();
    }
}
