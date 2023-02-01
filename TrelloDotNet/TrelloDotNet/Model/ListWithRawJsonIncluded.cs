using System.Collections.Generic;
using TrelloDotNet.Interface;

namespace TrelloDotNet.Model
{
    public class ListWithRawJsonIncluded<T> : List<T>, IRawJsonObject
    {
        public string RawJson { get; set; }
    }
}