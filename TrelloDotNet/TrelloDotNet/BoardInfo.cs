using System;
using System.Collections.Generic;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    //todo - WIP
    internal class BoardInfo<TList, TLabel> where TList : Enum where TLabel : Enum
    {
        private readonly Dictionary<TList, string> _listMappings;
        private readonly Dictionary<TLabel, string> _labelMappings;
        public string BoardId { get; }

        public BoardInfo(string boardId, Dictionary<TList, string> listMappings, Dictionary<TLabel, string> labelMappings)
        {
            BoardId = boardId;
            _listMappings = listMappings;
            _labelMappings = labelMappings;
        }

        public string GetListId(TList list)
        {
            if (!_listMappings.ContainsKey(list))
            {
                throw new ArgumentOutOfRangeException(nameof(list), $"List '{list}' does not seem to have been mapped in board-info");
            }
            return _listMappings[list];
        }

        public string GetLabelId(TLabel label)
        {
            if (!_labelMappings.ContainsKey(label))
            {
                throw new ArgumentOutOfRangeException(nameof(label), $"List '{label}' does not seem to have been mapped in board-info");
            }
            return _labelMappings[label];
        }
    }
}