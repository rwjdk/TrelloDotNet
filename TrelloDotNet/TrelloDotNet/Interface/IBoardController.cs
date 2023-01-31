using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    public interface IBoardController
    {
        Task<Board> GetAsync(string boardId);
        Task<List<List>> GetListsAsync(string boardId);
        Task<List<Label>> GetLabelsAsync(string boardId);
    }
}