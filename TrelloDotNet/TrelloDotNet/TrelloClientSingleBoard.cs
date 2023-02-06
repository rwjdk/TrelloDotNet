using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    //todo - WIP - Expose
    internal class TrelloClientSingleBoard<TList, TLabel> where TList : Enum where TLabel : Enum
    {
        private readonly BoardInfo<TList, TLabel> _boardInfo;
        private readonly TrelloClient _trelloClient;

        public TrelloClientSingleBoard(string apiKey, string token, BoardInfo<TList, TLabel> boardInfo)
        {
            _boardInfo = boardInfo;
            _trelloClient = new TrelloClient(apiKey, token);
        }

        public async Task<List<Card>> GetCardsOnListAsync(TList list)
        {
            return await _trelloClient.GetCardsInListAsync(_boardInfo.GetListId(list));
        }
    }
}