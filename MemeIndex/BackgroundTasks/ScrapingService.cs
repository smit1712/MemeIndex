using MemeIndex.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Scrapers;
using Scrapers.Events;
using Shared.Models;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MemeIndex.BackgroundTasks
{
    public class ScrapingService : IHostedService, IDisposable
    {
        private Timer _timer;
        private ILogger<ScrapingService> _logger;

        private IServiceScopeFactory _scopeFactory;

        private ILoggerFactory LoggerFactory;
        private ScaperController _scaperController;

        public ScrapingService(ILogger<ScrapingService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            LoggerFactory = new LoggerFactory();

            _scaperController = new ScaperController(new Logger<ScaperController>(LoggerFactory));

            _scaperController.DataReady += OnDataReady;
        }

        private void OnDataReady(object sender, OnScraperDataReady e)
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<MemeDBContext>();



                    foreach (Meme meme in e.Memes)
                    {
                        if (!ShouldMemeBeAdded(context, meme)) continue;

                        //Add meta data if not excisting
                        //foreach (MetaData metadata in meme.MetaData)
                        //{
                        //    if (!ShouldMetadataBeAdded(context, metadata)) continue;
                        //    context.MemeCategorys.Add(metadata);
                        //    context.SaveChanges();
                        //}

                        context.Memes.Add(meme);
                        context.SaveChanges();
                        //TODO: Check for dubbles

                    }
                }
            }
            catch (Exception ex)
            {
                //HandleGeneralException(ex);
            }
        }

        private static bool ShouldMetadataBeAdded(MemeDBContext context, MemeCategory metadata)
        {
            bool result = true;

            if (context.MemeCategorys.Any(m => m.Data == metadata.Data))
            {
                result = false;
            }
            //TODO: implement better duplicate checks
            return result;
        }


        private static bool ShouldMemeBeAdded(MemeDBContext context, Meme meme)
        {
            bool result = true;

            if (context.Memes.Any(m => m.Resource == meme.Resource))
            {
                result = false;
            }
            //TODO: implement better duplicate checks
            return result;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting ScrapingService");
            _timer = new Timer(o => _scaperController.StarScraping(), null, TimeSpan.Zero, TimeSpan.FromHours(12));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping ScrapingSerice");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
