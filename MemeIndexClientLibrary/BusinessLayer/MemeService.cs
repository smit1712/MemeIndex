using MemeIndexClientLibrary.DataLayer;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeIndexClientLibrary.BusinessLayer
{
    public class MemeService : IMemeService
    {
        private readonly IMemeIndexApiClient _apiClient;

        public MemeService(IMemeIndexApiClient ApiClient)
        {
            _apiClient = ApiClient;
        }

        public async Task<ICollection<Meme>> GetAllMemes()
        {
            try
            {
                return await _apiClient.GetAllMemes();
            }
            catch (Exception ex)
            {
                HandleGeneralException(ex);
                return null;
            }
        }

        public async Task<ICollection<Meme>> GetCategoriesWithMaxAmount(int maxAmount)
        {
            try
            {
                return await _apiClient.GetCategoriesWithMaxAmount(maxAmount);
            }
            catch (Exception ex)
            {
                HandleGeneralException(ex);
                return null;
            }
        }

        public async Task<ICollection<Meme>> GetMemesByCategory(string Category)
        {
            try
            {
                return await _apiClient.GetMemesByCategory(Category);
            }
            catch (Exception ex)
            {
                HandleGeneralException(ex);
                return null;
            }
        }

        public async Task<ICollection<Meme>> GetMemesWithMaxAmount(int maxAmount)
        {
            try
            {
                return await _apiClient.GetMemesWithMaxAmount(maxAmount);
            }
            catch (Exception ex)
            {
                HandleGeneralException(ex);
                return null;
            }
        }

        private void HandleGeneralException(Exception ex)
        {
            //TODO: implement exception handeling
            throw ex;
        }
    }
}
