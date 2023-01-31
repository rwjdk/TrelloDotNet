using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    public interface IListController
    {
        Task<List> GetAsync(string listId);
        Task<List> AddAsync(Board board, string name);
        Task<List> AddAsync(string longBoardId, string name);
    }
}