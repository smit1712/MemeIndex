using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeIndexClientLibrary.BusinessLayer
{
    public interface IMemeService
    {
        Task<ICollection<Meme>> GetAllMemes();
        Task<ICollection<Meme>> GetMemesWithMaxAmount(int maxAmount);
        Task<ICollection<Meme>> GetMemesByCategory(string category);
        Task<ICollection<Meme>> GetCategoriesWithMaxAmount(int maxAmount);
    }
}
