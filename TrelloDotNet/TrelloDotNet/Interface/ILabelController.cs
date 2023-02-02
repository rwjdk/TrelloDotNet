using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    /// <summary>
    /// Controller of Label-specific Methods
    /// </summary>
    public interface ILabelController
    {
        //todo - Need to Have

        //todo - Nice to Have
        //- Label CRUD

        /// <summary>
        /// Get List of Labels defined for a board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <returns>List of Labels</returns>
        Task<List<Label>> GetLabelsOfBoardAsync(string boardId);
    }
}