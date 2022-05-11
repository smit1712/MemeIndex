using MemeIndexClientLibrary.BusinessLayer;
using MemeIndexClientLibrary.DataLayer;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MemeIndexClientLibrary
{
    public class MemeIndexClient
    {
        public IMemeService MemeService;
        public MemeIndexClient()
        {
            Main();
        }

        public void Main()
        {
            var serviceProvider = new ServiceCollection()
                    .AddSingleton<IMemeService, MemeService>()
                    .AddSingleton<IMemeIndexApiClient, MemeIndexApiClient>()
                    .AddSingleton<IMemeIndexStorageService, MemeIndexStorageService>()
                    .BuildServiceProvider(); 

            MemeService = serviceProvider.GetService<IMemeService>();
        }
    }
}