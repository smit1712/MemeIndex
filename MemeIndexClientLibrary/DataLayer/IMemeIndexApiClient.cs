using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeIndexClientLibrary.DataLayer
{
    public interface IMemeIndexApiClient
    {
        Task<ICollection<Meme>> GetAllMemes();
        Task<ICollection<Meme>> GetMemesWithMaxAmount(int maxAmount);
        Task<ICollection<Meme>> GetMemesByCategory(string Category);
        Task<ICollection<Meme>> GetCategoriesWithMaxAmount(int maxAmount);
    }
}
