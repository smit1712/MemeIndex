using Shared.Models;
using System;
using System.Collections.Generic;

namespace Scrapers.Events
{
    public class OnScraperDataReady : EventArgs
    {
        public List<Meme> Memes { get; set; }
    }
}
