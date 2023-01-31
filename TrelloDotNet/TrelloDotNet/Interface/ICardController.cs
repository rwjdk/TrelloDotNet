using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    public interface ICardController
    {
        Task<Card> GetAsync(string cardId);
    }
}