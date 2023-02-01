using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    public interface ICardController
    {
        Task<Card> GetAsync(string cardId);

        Task<Card> UpdateAsync( 
            string cardId,
            string newName = null,
            string newDescription = null,
            bool? closed = null,
            List<string> memberIds = null,
            string newListId = null,
            List<string> labelsIds = null,
            string newLongBoardId = null,
            int? position = null,
            DateTimeOffset? due = null,
            DateTimeOffset? start = null,
            bool? dueComplete = null,
            bool? subscribed = null 
        );
    }
}