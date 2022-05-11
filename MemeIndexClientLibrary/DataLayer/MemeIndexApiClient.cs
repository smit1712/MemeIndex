using Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MemeIndexClientLibrary.DataLayer
{
    public class MemeIndexApiClient : IMemeIndexApiClient
    {
        private HttpClient _client;
        private readonly string _serverIp;
        private readonly int _port;

        public MemeIndexApiClient(string serverIp = "192.168.2.17", int port = 5024)
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            _client = new HttpClient(handler);

            _serverIp = serverIp;
            _port = port;
        }

        public async Task<ICollection<Meme>> GetAllMemes()
        {
            var result = await _client.GetFromJsonAsync<List<Meme>>($"https://{_serverIp}:{_port}/api/Meme/GetAllMemes");

            return result;
        }

        public async Task<ICollection<Meme>> GetMemesWithMaxAmount(int maxAmount)
        {
            if (maxAmount == 0 ||
                maxAmount < 0) throw new ArgumentException("The maxAmount is zero or less then zero.");

            var result = await _client.GetFromJsonAsync<List<Meme>>($"https://{_serverIp}:{_port}/api/Meme/GetMemesWithMaxAmount?maxAmount={maxAmount}");

            return result;
        }

        public async Task<ICollection<Meme>> GetMemesByCategory(string Category)
        {
            if (string.IsNullOrEmpty(Category)) throw new ArgumentException("The Category is not specified");

            var result = await _client.GetFromJsonAsync<List<Meme>>($"https://{_serverIp}:{_port}/api/Meme/GetMemesByCategory?Category={Category}");

            return result;
        }

        public async Task<ICollection<Meme>> GetCategoriesWithMaxAmount(int maxAmount)
        {
            if (maxAmount == 0 ||
                maxAmount < 0) throw new ArgumentException("The maxAmount is zero or less then zero.");

            var result = await _client.GetFromJsonAsync<List<Meme>>($"https://{_serverIp}:{_port}/api/Meme/GetCategoriesWithMaxAmount?maxAmount={maxAmount}");

            return result;
        }
    }
}
